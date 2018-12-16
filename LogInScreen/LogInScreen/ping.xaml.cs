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
        //I have added the namespace using System.Net.NetworkInformation; so as to have access to the 
        //required networking commands in the code.

        public ping()
        {
            InitializeComponent();
        }

        //Ping test takes the input from the text box and sets it to the 's' veriable
        // 'r' the ping reply is the address sent by the ping send command.
        // the returned values if successful are set as strings output in a text block with additional text.       
        private void BtnPing_Click(object sender, RoutedEventArgs e)
        {
            Ping p = new Ping();
            PingReply r;

            String s = tbxHost.Text;

            //When I do this line on a page 'Send' generates an exception for some reason.
            //Using a new window for now.
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
