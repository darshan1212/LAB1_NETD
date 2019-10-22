// Original Author: Kyle Chapman
// Editor: Darshan Patel
// Date: 18 Sept 2019
// Lab 1 NetD 3201
// Description: This file contain PieceworkWorker class that is required in the main application xaml file.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LAB1_NETD // Ensure this namespace matches your own
{
    class PieceworkWorker
    {

        #region "Variable declarations"

        // Instance variables
        private int employeeId;
        private string employeeFirstName;
        private string employeeLastName;
        private int employeeMessages;
        private decimal employeePay;
        private DateTime entryDate;



        // constant variables

        private const int UPPER_BOUND = 20000;

        #endregion

        #region "Constructors"

        

        /// <summary>
        /// PieceworkWorker constructor: accepts a worker's name and number of
        /// messages, sets and calculates values as appropriate.
        /// </summary>
        /// <param name="firstNameValue">the worker's name</param>
        /// <param name="lastNameValue">the worker's name</param>
        /// <param name="messageValue">a worker's number of messages sent</param>
        public PieceworkWorker(string firstNameValue,string lastNameValue, string messagesValue)
        {
            // Validate and set the worker's first name
            this.FirstName = firstNameValue;
            // Validate and set the worker's last name
            this.LastName = lastNameValue;
            // Validate Validate and set the worker's number of messages
            this.Messages = messagesValue;
            // Calculcate the worker's pay and update all summary values
            findPay();
            // getting current date and time
            this.EntryDate = DateTime.Now;

            DBL.InsertNewRecord(this);
        }

        /// <summary>
        /// PieceworkWorker constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        public PieceworkWorker()
        {
            
        }

       

        #endregion

        #region "Class methods"

        /// <summary>
        /// Currently called in the constructor, the findPay() method is
        /// used to calculate a worker's pay using threshold values to
        /// change how much a worker is paid per message. This also updates
        /// all summary values.
        /// </summary>
        private void findPay()
        {
            // TO DO
            // Fill in this entire method by following the instructions provided
            // in the NETD 3202 Lab 1 handout
            // It is suggested that you use the requirements as a checklist in
            // order to ensure you don't miss any requirements.


            // decalring constants Source: Lab 2 Solution provided by Prof. Kyle Chapman

            // Constants representing the various pay thresholds
            const int MinimumCap = 1;
            const int ThresholdCapLowest = 2499;
            const int ThresholdCapLow = 4999;
            const int ThresholdCapMedium = 7499;
            const int ThresholdCapHigh = 9999;

            // Constants representing the various pay rates
            const decimal PayRateLowest = 0.018M;
            const decimal PayRateLow = 0.024M;
            const decimal PayRateMedium = 0.03M;
            const decimal PayRateHigh = 0.035M;
            const decimal PayRateHighest = 0.04M;


            
            switch (employeeMessages)
            {
                // source: https://stackoverflow.com/questions/20147879/switch-case-can-i-use-a-range-instead-of-a-one-number

                // business logic for calculating employee pay
                case int i when (i >= MinimumCap && i <= ThresholdCapLowest):
                    employeePay = PayRateLowest * employeeMessages;
                    break;

                case int i when (i >= ThresholdCapLowest+1 && i <= ThresholdCapLow):
                    employeePay = PayRateLow * employeeMessages;
                    break;

                case int i when (i >= ThresholdCapLow+1 && i <= ThresholdCapMedium):
                    employeePay = PayRateMedium * employeeMessages;

                    break;

                case int i when (i >= ThresholdCapMedium+1 && i <= ThresholdCapHigh+1):
                    employeePay = PayRateHigh * employeeMessages;

                    break;

                case int i when (i > ThresholdCapHigh+1):
                    employeePay = PayRateHighest * employeeMessages;

                    break;

            }


         


        }

        /// <summary>
        /// Updating all three shared values 
        /// </summary>
    
      

        #endregion

        #region "Property Procedures"

        /// <summary>
        /// Gets and sets a worker's First name
        /// </summary>
        /// <returns>an employee's first name</returns>
        internal string FirstName
        {
            get
            {
                return employeeFirstName;
            }
            set
            {
             
                // validating if the string is empty and also checking if there are more than 2 aphabets in the string
                // source: https://stackoverflow.com/questions/12884610/how-to-check-if-a-string-contains-any-letter-from-a-to-z

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("FirstName", "Do not leave this empty");
                }
                else if (Regex.Matches(value, @"[a-zA-Z]").Count < 2)
                {
                    throw new ArgumentException("First Name must contain atleast 2 alphabets", "FirstName");
                }
                else employeeFirstName = value;
                
            }
        }


        /// <summary>
        /// Gets and sets a worker's  Last name
        /// </summary>
        /// <returns>an employee's last name</returns>
        internal string LastName
        {
            get
            {
                return employeeLastName;
            }
            set
            {

                // validating if the string is empty and also checking if there are more than 2 aphabets in the string
                // source: https://stackoverflow.com/questions/12884610/how-to-check-if-a-string-contains-any-letter-from-a-to-z

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("LastName", "Do not leave this empty");
                }
                else if (Regex.Matches(value, @"[a-zA-Z]").Count < 2)
                {
                    throw new ArgumentException("Last Name must contain atleast 2 alphabets", "LastName");
                }
                else employeeLastName = value;

            }
        }


        /// <summary>
        /// Gets and sets the number of messages sent by a worker
        /// </summary>
        /// <returns>an employee's number of messages</returns>
        internal string Messages
        {
            get
            {
                return employeeMessages.ToString();
            }
            set
            {
                
              

                if(String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Message", "You cannot leave this empty");
                }
                else if(!int.TryParse(value, out employeeMessages))
                {
                    throw new ArgumentException("You must enter numeric value", "Message");
                }
                else if (employeeMessages > UPPER_BOUND)
                {
                    throw new ArgumentOutOfRangeException("Message","Please, Not more than "+UPPER_BOUND+"!");
                }
                else if(employeeMessages<0)
                {
                    throw new ArgumentException("Messages must be greater than 0","Message");
                }


            }
        }

        /// <summary>
        /// Gets the worker's pay
        /// </summary>
        /// <returns>a worker's pay</returns>
        internal decimal Pay
        {
            get
            {
                return employeePay;
            }
            set
            {
                employeePay = value;
            }
        }

        /// <summary>
        /// Gets the worker's Id
        /// </summary>
        /// <returns>a worker's Id</returns>
        internal int Id
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
            }
        }

        /// <summary>
        /// Gets the worker's Entry Date
        /// </summary>
        /// <returns>a worker's EntryDate</returns>
        internal DateTime EntryDate
        {
            get
            {
                return entryDate;
            }
            set
            {
                entryDate = value;
            }
        }

        /// <summary>
        /// Gets the overall total pay among all workers
        /// </summary>
        /// <returns>the overall total pay among all workers</returns>
        internal static decimal TotalPay
        {
            get
            {
                return  Convert.ToDecimal(DBL.GetTotal(DBL.Totals.Pay));
            }
        }

        /// <summary>
        /// Gets the overall number of workers
        /// </summary>
        /// <returns>the overall number of workers</returns>
        internal static int TotalWorkers
        {
            get
            {
                return  Convert.ToInt32(DBL.GetTotal(DBL.Totals.Employees));

            }
        }

        /// <summary>
        /// Gets the overall number of messages sent
        /// </summary>
        /// <returns>the overall number of messages sent</returns>
        internal static int TotalMessages
        {
            get
            {
                return Convert.ToInt32(DBL.GetTotal(DBL.Totals.Messages));
            }
        }

        /// <summary>
        /// Calculates and returns an average pay among all workers
        /// </summary>
        /// <returns>the average pay among all workers</returns>
        internal static decimal AveragePay
        {
            get
            {
                // TO DO
                // Write the logic that will return the average pay among all workers. Test this carefully!
                decimal tempDecimal;
                if (TotalWorkers == 0)  tempDecimal = 0;
                else
                {
                    tempDecimal = TotalPay / TotalWorkers;
                }

                return tempDecimal;
                
            }
        }

        /// <summary>
        /// Returns a list of all workers in the database as a DataTable
        /// </summary>
        /// <returns>a DataTable containing all worker objects</returns>
        internal static DataTable AllWorkers
        {
            get
            {
                return DBL.GetEmployeeList();
            }
        }

        #endregion

    }
}


//////////////•	1 bonus stock if a block comment is included at the very bottom of your
///worker class code file that briefly answers the question, “Why are some property procedures
///in this class read/write while others are read-only?”, including reference to at least one example of each from this lab
///
/////////////////////////Answer////////////////////////////////////
// For this program, if we look according to business perspective the only thing that is changed by the user is 
// the name and the messages, so those must have a set and the get values. Creating unnecessary get and set without requirements
// may cause security issues and it hinders the class definition. So the OOP concepts play important part here.
