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
        
        public SummaryForm()
        {
            InitializeComponent();

            lblTotalWorkers.Content = PieceworkWorker.TotalWorkers;
            lblToalMessages.Content = PieceworkWorker.TotalMessages;
            lblTotalPay.Content = "$ " + string.Format("{0:0.00}", PieceworkWorker.TotalPay);
            lblAveragePay.Content = "$ " + string.Format("{0:0.00}", PieceworkWorker.AveragePay);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
