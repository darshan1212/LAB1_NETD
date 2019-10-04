// Author: Darshan Patel
// Last Modified: 04 Oct 2019
// Lab 2 NetD 3201
// Description: Main form which will be loaded first to get input of worker name and his or her messages.



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

        /// <summary>
        /// This is event handler for the calculate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalculatePay_Click(object sender, RoutedEventArgs e)
        {
            // Calling piece worker with name and message as input
            try
            {

                    PieceworkWorker pieceworkWorker = new PieceworkWorker(txtWorkerName.Text,txtMessageSent.Text);
              

                    //Show the output in labels
                    lblResultTotalPay.Content = "$ " + string.Format("{0:0.00}", pieceworkWorker.Pay, 2);

                  
                    // disable calculate button and entry fields
                    btnCalculatePay.IsEnabled = false;
                    txtMessageSent.IsEnabled = false;
                    txtWorkerName.IsEnabled = false;


                    // focus on the clear button
                    btnClearFields.Focus();
               
            }
            catch(ArgumentNullException ex)
            {
                if(ex.ParamName=="Name")
                {
                    lblWorkerNameError.Content = ex.Message;
                    txtWorkerName.Background = Brushes.Red;
                    txtWorkerName.Focus();
                }
                else if(ex.ParamName=="Message")
                {
                    lblMessageSentError.Content = ex.Message;
                    txtWorkerName.Background = Brushes.Red;
                    txtMessageSent.Focus();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (ex.ParamName == "Name")
                {
                    lblWorkerNameError.Content = ex.Message;
                    txtWorkerName.Background = Brushes.Red;
                    txtWorkerName.Focus();
                }
                if (ex.ParamName == "Message")
                {
                    lblMessageSentError.Content = ex.Message;
                    txtMessageSent.Background = Brushes.Red;

                    txtMessageSent.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName == "Name")
                {
                    lblWorkerNameError.Content = ex.Message;
                    txtWorkerName.Background = Brushes.Red;
                    txtWorkerName.Focus();
                }
                if (ex.ParamName == "Message")
                {
                    lblMessageSentError.Content = ex.Message;
                    txtMessageSent.Background = Brushes.Red;
                    txtMessageSent.Focus();
                }
            }
            catch (Exception excep)
            {
                // showing the message that cause interruption
                MessageBox.Show(excep.Message);
                //focus back on the buttons
                btnClearFields.Focus();
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
            lblResultTotalPay.Content = "";

        }

        /// <summary>
        /// it opens Summary Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSummary_Click(object sender, RoutedEventArgs e)
        {
            (new SummaryForm()).Show();
            
        }

        /// <summary>
        /// Make Name textbox colour as it is
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtWorkerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblWorkerNameError.Content = String.Empty;
            txtWorkerName.Background = Brushes.White;
        }

        /// <summary>
        /// Make entry textbox as it is
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMessageSent_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblMessageSentError.Content = String.Empty;
            txtMessageSent.Background = Brushes.White;
        }

        private void TxtWorkerName_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
