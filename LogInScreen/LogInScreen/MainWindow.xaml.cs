using DBLibrary;
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

namespace LogInScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel.csdl|res://*/BloodDonorModel.ssdl|res://*/BloodDonorModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");
        public MainWindow()
        {
            InitializeComponent();
            
        }

        //Click event for the close button
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        //Click event for the OK button
        //Checks the username and password entered against the record in the database
        //The dashboard.user = user line is sending the user to the dashboard as part of controlling what the user can see and do
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;           
            foreach (var user in db.Users)
            {
                if (user.Username == currentUser && user.Password == currentPassword)
                {
                    Dashboard dashboard = new Dashboard();                    
                    dashboard.user = user;                    
                    dashboard.ShowDialog();                    
                    this.Hide();                   
                }
                else
                {
                    lblErrorMessage.Content = "Please check your login details";
                }
            }
        }
    }
}
