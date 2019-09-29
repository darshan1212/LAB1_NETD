// Author: Darshan Patel
// Date: 18 Sept 2019
// Lab 1 NetD 3201

using System;
using System.Collections.Generic;
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
        private string employeeName;
        private int employeeMessages;
        private decimal employeePay;

       

        // Shared class variables
        private static int overallNumberOfEmployees = 0;
        private static int overallMessages = 0;
        private static decimal overallPayroll = 0;

        // constant variables

        private const int UPPER_BOUND = 20000;

        #endregion

        #region "Constructors"

        /// <summary>
        /// PieceworkWorker constructor: accepts a worker's name and number of
        /// messages, sets and calculates values as appropriate.
        /// </summary>
        /// <param name="nameValue">the worker's name</param>
        /// <param name="messageValue">a worker's number of messages sent</param>
        public PieceworkWorker(string nameValue, string messagesValue)
        {
            // Validate and set the worker's name
            this.Name = nameValue;
            // Validate Validate and set the worker's number of messages
            this.Messages = messagesValue;
            // Calculcate the worker's pay and update all summary values
            findPay();
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

            switch (employeeMessages)
            {
                // source: https://stackoverflow.com/questions/20147879/switch-case-can-i-use-a-range-instead-of-a-one-number

                // business logic for calculating employee pay
                case int i when (i >= 1 && i <= 2499):
                    employeePay = (decimal)(0.018 * employeeMessages);
                    break;

                case int i when (i >= 2500 && i <= 4999):
                    employeePay = (decimal)(0.024 * employeeMessages);
                    break;

                case int i when (i >= 5000 && i <= 7499):
                    employeePay = (decimal)(0.03 * employeeMessages);

                    break;

                case int i when (i >= 7500 && i <= 10000):
                    employeePay = (decimal)(0.035 * employeeMessages);

                    break;

                case int i when (i > 10000):
                    employeePay = (decimal)(0.04 * employeeMessages);

                    break;

            }


            // updating the shared variables
            overallNumberOfEmployees++;
            overallPayroll += employeePay;
            overallMessages += employeeMessages;



        }

        /// <summary>
        /// Updating all three shared values 
        /// </summary>
    
      

        #endregion

        #region "Property Procedures"

        /// <summary>
        /// Gets and sets a worker's name
        /// </summary>
        /// <returns>an employee's name</returns>
        public string Name
        {
            get
            {
                return employeeName;
            }
            set
            {
             
                // validating if the string is empty and also checking if there are more than 2 aphabets in the string
                // source: https://stackoverflow.com/questions/12884610/how-to-check-if-a-string-contains-any-letter-from-a-to-z

                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name", "Do not leave this empty");
                }
                else if (Regex.Matches(value, @"[a-zA-Z]").Count < 2)
                {
                    throw new ArgumentException("Name must contain atleast 2 alphabets", "Name");
                }
                else employeeName = value;
                
            }
        }

        /// <summary>
        /// Gets and sets the number of messages sent by a worker
        /// </summary>
        /// <returns>an employee's number of messages</returns>
        public string Messages
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
        public decimal Pay
        {
            get
            {
                return employeePay;
            }
        }

        /// <summary>
        /// Gets the overall total pay among all workers
        /// </summary>
        /// <returns>the overall total pay among all workers</returns>
        public static decimal TotalPay
        {
            get
            {
                return overallPayroll;
            }
        }

        /// <summary>
        /// Gets the overall number of workers
        /// </summary>
        /// <returns>the overall number of workers</returns>
        public static int TotalWorkers
        {
            get
            {
                return overallNumberOfEmployees;
            }
        }

        /// <summary>
        /// Gets the overall number of messages sent
        /// </summary>
        /// <returns>the overall number of messages sent</returns>
        public static int TotalMessages
        {
            get
            {
                return overallMessages;
            }
        }

        /// <summary>
        /// Calculates and returns an average pay among all workers
        /// </summary>
        /// <returns>the average pay among all workers</returns>
        public static decimal AveragePay
        {
            get
            {
                // TO DO
                // Write the logic that will return the average pay among all workers. Test this carefully!
                decimal tempDecimal;
                if (overallNumberOfEmployees == 0)  tempDecimal = 0;
                else
                {
                    tempDecimal = overallPayroll / overallNumberOfEmployees;
                }

                return tempDecimal;
                
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
