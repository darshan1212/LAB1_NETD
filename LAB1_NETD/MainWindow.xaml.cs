// Author: Darshan Patel
// Date: 18 Sept 2019
// Lab 1 NetD 3201


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB1_NETD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //focusing the first element of the form
            txtWorkerName.Focus();
        }

        private void BtnCalculatePay_Click(object sender, RoutedEventArgs e)
        {
            // Calling piece worker with name and message as input
            PieceworkWorker pieceworkWorker = new PieceworkWorker(txtWorkerName.Text,txtMessageSent.Text);
            // Check if user entered input are valid or not
            if (pieceworkWorker.isObjectValid)
            {
                //Show the output in labels
                lblResultTotalPay.Content = "$ " + string.Format("{0:0.00}", pieceworkWorker.Pay, 2);

                // updating shared values
                pieceworkWorker.UpdateSharedValues();

                //Show the output in labels
                lblResultWorkersTotalPay.Content = "$ " + string.Format("{0:0.00}", PieceworkWorker.TotalPay);
                lblResultAveragePay.Content = "$ " + string.Format("{0:0.00}", PieceworkWorker.AveragePay);

                // disable calculate button and entry fields
                btnCalculatePay.IsEnabled = false;
                txtMessageSent.IsEnabled = false;
                txtWorkerName.IsEnabled = false;


                // focus on the clear button
                btnClearFields.Focus();


            }
            else
            {
                //if not valid then show the error message in message box.
                MessageBox.Show("Either your name is empty or your entered wrong number of messages. The name field should not be " +
                    "empty and please ensure that number of messages should be whole integer number greater than Zero\n\n\n" + "Is your name contains atlease 2 alphabets?");
                Reset();
            }

            
        }

       
        /// <summary>
        /// resetting two upper text fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearFields_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            //re-enable
            btnCalculatePay.IsEnabled = true;
            txtMessageSent.IsEnabled = true;
            txtWorkerName.IsEnabled = true;
            // focus the first field
            txtWorkerName.Focus();


        }

        /// <summary>
        /// Exiting button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// it clears the messages and a helper funtion of the reset button
        /// </summary>
        private void Reset()
        {
            // routing for resetting all five dynamic fields
            txtMessageSent.Clear();
            txtWorkerName.Clear();
            lblResultTotalPay.Content = "$ 0.00";

        }
    }
}
