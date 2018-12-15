using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.NetworkInformation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogInScreen
{
    /// <summary>
    /// Interaction logic for ping.xaml
    /// </summary>
    public partial class ping : Window
    {
        public ping()
        {
            InitializeComponent();
        }

        private void BtnPing_Click(object sender, RoutedEventArgs e)
        {
            Ping p = new Ping();
            PingReply r;

            String s = tbxHost.Text;

            r = p.Send(s);



            if (r.Status == IPStatus.Success)
            {

                tbxPingOutput.Text = "Ping to " + s.ToString() + "[" + r.Address.ToString() + "] successful - " + r.Buffer.Length.ToString() + " bytes in " + r.RoundtripTime.ToString() + " ms." + "\n";
                tbxHost.Text = "";

            }
            else
            {
                tbxPingOutput.Text = "Host cannot be reached";
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {   
            //using this.Close here closes the whole system, hiding this instead for now.
            this.Hide(); 

            
        }
    }

    
}
