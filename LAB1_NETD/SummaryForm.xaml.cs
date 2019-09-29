// Author: Darshan Patel
// Date: 29 Sept 2019
// Lab 2 NetD 3201

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
using System.Windows.Shapes;
using LAB1_NETD;

namespace LAB1_NETD
{
    /// <summary>
    /// Interaction logic for SummaryForm.xaml
    /// </summary>
    public partial class SummaryForm : Window
    {
        /// <summary>
        /// Constructor of summariy form which will display all info.
        /// </summary>        
        public SummaryForm()
        {
            InitializeComponent();

            // Populating labels with respective values.
            lblTotalWorkers.Content = PieceworkWorker.TotalWorkers;
            lblToalMessages.Content = PieceworkWorker.TotalMessages;
            lblTotalPay.Content = "$ " + string.Format("{0:0.00}", PieceworkWorker.TotalPay);
            lblAveragePay.Content = "$ " + string.Format("{0:0.00}", PieceworkWorker.AveragePay);

            btnCloseSummary.Focus();
        }

        /// <summary>
        /// Closes the Summary form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
