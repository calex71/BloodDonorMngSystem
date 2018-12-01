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

namespace LogInScreen
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void MnuShowGroups_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu option is checked");
        }

        private void mnuHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("For technical support and assistance please use the below contacts:" + Environment.NewLine + Environment.NewLine + "Phone: 555-9367" + Environment.NewLine + "Email: helpdesk@blood-donor.com", "Help Message", MessageBoxButton.OK, MessageBoxImage.Information);


        }
    }
}
