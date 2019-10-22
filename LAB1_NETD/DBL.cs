// DBL.cs
//         Title: DBL - Data Base Layer for Piecework Payroll
// Last Modified: 
//    Written By: Kyle Chapman
// Adapted from PieceworkWorker by Kyle Chapman, October 2019
// 
// This is a module with a set of classes allowing for interaction between
// Piecework Worker data objects and a database.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LAB1_NETD;

namespace LAB1_NETD
{
    class DBL
    {

        #region "Connection String"

        /// <summary>
        /// Return connection string
        /// </summary>
        /// <returns>Connection string</returns>
        private static string GetConnectionString()
        {
            return LAB1_NETD.Properties.Settings.Default.connectionString;
        }

        #endregion

        #region "Constants for Totals"

        /// <summary>
        /// Document values used for returning totals (so as to not identify them as arbitrary strings)
        /// These can be used within the assembly by calling DBL.Totals.Employees, etc.
        /// </summary>
        internal class Totals
        {
            internal const string Employees = "Employees";
            internal const string Messages = "Messages";
            internal const string Pay = "Pay";
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Function used to select one worker from database, uses workerId as the primary key
        /// </summary>
        /// <param name="id">an id representing workers stored in the database</param>
        /// <returns>a worker object</returns>
        internal static PieceworkWorker GetOneRow(int workerId)
        {
            // Declare new worker object
            PieceworkWorker returnWorker = new PieceworkWorker();

            // Declare new SQL connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command
            SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM [tblEntries] WHERE [EntryId] = " + workerId, dbConnection);

            // Try to connect to the database, create a datareader. If successful, read from the database and fill created row
            // with information from matching record
            try
            {
                dbConnection.Open();
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    returnWorker = new PieceworkWorker(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    returnWorker.Id = workerId;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("A database error has been encountered: " + Environment.NewLine + ex.Message, "Database Error");
            }
            finally
            {
                dbConnection.Close();
            }

            // Return the populated row
            return returnWorker;

        }

        /// <summary>
        /// Function that returns all workers in the database as a DataTable for display
        /// </summary>
        /// <returns>a DataTable containing all workers in the database</returns>
        internal static DataTable GetEmployeeList()
        {
            // Declare the connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command
            SqlCommand command = new SqlCommand("SELECT * FROM [tblEntries]", dbConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            // Declare a DataTable object that will hold the return value
            DataTable employeeTable = new DataTable();

            // Try to connect to the database, and use the adapter to fill the table
            try
            {
                dbConnection.Open();
                adapter.Fill(employeeTable);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("A database error has been encountered: " + Environment.NewLine + ex.Message, "Database Error");
            }
            finally
            {
                dbConnection.Close();
            }

            // Return the populated DataTable
            return employeeTable;
        }

        /// <summary>
        /// Function to add a new worker to the worker database
        /// </summary>
        /// <param name="insertWorker">a worker object to be inserted</param>
        /// <returns>true if successful</returns>
        internal static bool InsertNewRecord(PieceworkWorker insertWorker)
        {
            // Create return value
            bool returnValue = false;

            // Declare the connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command and assign it paramaters
            SqlCommand command = new SqlCommand("INSERT INTO tblEntries VALUES(@firstName, @lastName, @messages, @pay, @entryDate)", dbConnection);
            command.Parameters.AddWithValue("@firstName", insertWorker.FirstName);
            command.Parameters.AddWithValue("@lastName", insertWorker.LastName);
            command.Parameters.AddWithValue("@messages", insertWorker.Messages);
            command.Parameters.AddWithValue("@pay", insertWorker.Pay);
            command.Parameters.AddWithValue("@entryDate", insertWorker.EntryDate);

            // Try to insert the new record, return result
            try
            {
                dbConnection.Open();
                returnValue = (command.ExecuteNonQuery() == 1);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("A database error has been encountered: " + Environment.NewLine + ex.Message, "Database Error");
            }
            finally
            {
                dbConnection.Close();
            }

            // Return the true if this worked, false if it failed
            return returnValue;
        }

        /// <summary>
        /// Function to update an existing worker in the worker database; if they don't exist, it instead attempts to add this worker as a new worker
        /// </summary>
        /// <param name="updateWorker">a worker object to be updated</param>
        /// <returns>true if successful</returns>
        internal static bool UpdateExistingRow(PieceworkWorker updateWorker)
        {
            // Create return value
            bool returnValue = false;

            // If the worker exists, create dbConnection
            if (updateWorker.Id > 0)
            {
                // Declare the connection
                SqlConnection dbConnection = new SqlConnection(GetConnectionString());

                // Create new SQL command and assign it paramaters
                SqlCommand command = new SqlCommand("UPDATE tblEntries Set FirstName = @firstName, LastName = @lastName, Messages = @messages, Pay = @pay WHERE EntryId = @entryId", dbConnection);
                command.Parameters.AddWithValue("@workerId", updateWorker.Id);
                command.Parameters.AddWithValue("@firstName", updateWorker.FirstName);
                command.Parameters.AddWithValue("@lastName", updateWorker.LastName);
                command.Parameters.AddWithValue("@messages", updateWorker.Messages);
                command.Parameters.AddWithValue("@pay", updateWorker.Pay);
                command.Parameters.AddWithValue("@entryDate", updateWorker.EntryDate);

                // Try to open a connection to the database and update the record. Return result.
                try
                {
                    dbConnection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        returnValue = true;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("A database error has been encountered: " + Environment.NewLine + ex.Message, "Database Error");
                }
                finally
                {
                    dbConnection.Close();
                }

            }

            // If the worker does not exist, attempt to insert it instead
            else
            {
                if (InsertNewRecord(updateWorker))
                {
                    returnValue = true;
                }
            }

            // Returns true if the query executed; always false if the row is invalid
            return returnValue;
        }

        /// <summary>
        /// This method returns totals depending on what input is provided. For example
        /// providing 'Messages' as an argument returns total messages
        /// </summary>
        /// <param name="input">"Employees" returns total employees, "Messages" returns total messages, "Pay" returns total pay</param>
        /// <returns>string form of specified total</returns>
        internal static string GetTotal(string input)
        {
            // Declare the connection
            SqlConnection dbConnection = new SqlConnection(GetConnectionString());

            // Create new SQL command
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandType = CommandType.Text;

            // Check which total is required and set the statement accordingly
            if (input == Totals.Employees)
            {
                command.CommandText = "SELECT COUNT(EntryId) FROM tblEntries";
            }
            else if (input == Totals.Messages)
            {
                command.CommandText = "SELECT SUM(Messages) FROM tblEntries";
            }
            else if (input == Totals.Pay)
            {
                command.CommandText = "SELECT SUM(Pay) FROM tblEntries";
            }
            else
            {
                return "ERROR";
            }

            // Try to open a connection to the database and read the total. Return result.

            try
            {
                dbConnection.Open();
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("A database error has been encountered: " + Environment.NewLine + ex.Message, "Database Error");
            }
            finally
            {
                dbConnection.Close();
            }
            // If something unexpected occured, set the result as error
            return "ERROR";
        }

        #endregion

    }
}

