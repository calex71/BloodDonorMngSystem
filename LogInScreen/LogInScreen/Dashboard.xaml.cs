using DBLibrary1;
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
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel1.csdl|res://*/BloodDonorModel1.ssdl|res://*/BloodDonorModel1.msl;provider=System.Data.SqlClient;provider connection string=';data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");

        //This takes the user details from the login screen for user here in 
        //the private void CheckUserAccess(User user) method for access control.
        public User user = new User();

        public Dashboard()
        {   
            InitializeComponent();
        }

        //Click event for the exit button on the application menu.
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }     

        //Click event for the application/help menu
        private void mnuHelp_Click(object sender, RoutedEventArgs e)
        {        
            MessageBox.Show("For technical support and assistance please use the below contacts:" + Environment.NewLine + Environment.NewLine + "Phone: 555-9367" + Environment.NewLine + "Email: helpdesk@blood-donor.com", "Help Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //click event for the Admin/manage users submenu
        private void MnuManageUsersMenu_Click(object sender, RoutedEventArgs e)
        {
            admin admin = new admin();
            frmMain.Navigate(admin);
        }


        //Access level check to determin what the user sees and has access to.
        //Based on a check of their access LevelID.
        private void CheckUserAccess(User user)
        {

            if (user.LevelID == 1)
            {
                mnuToolsMenu.Visibility = Visibility.Visible;
            }
            if (user.LevelID == 3)
            {
                mnuAdminMenu.Visibility = Visibility.Visible;
                mnuToolsMenu.Visibility = Visibility.Visible;
                mnuPingTest.Visibility = Visibility.Visible;
                mnuUserAnalysis.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
            
        }

        private void MnuManageDonorsMenu_Click(object sender, RoutedEventArgs e)
        {
            bdonor bdonor = new bdonor();
            frmMain.Navigate(bdonor);
        }

        private void MnuPingTest_Click(object sender, RoutedEventArgs e)
        {
            ping ping = new ping();
            ping.Show();                       
        }

        private void MnuUserAnalysis_Click(object sender, RoutedEventArgs e)
        {
            UserAnalysis userAnalysis = new UserAnalysis();
            frmMain.Navigate(userAnalysis);
        }

        private void MnuDonorAnalysis_Click(object sender, RoutedEventArgs e)
        {
            DonerAnalysis donerAnalysis = new DonerAnalysis();
            frmMain.Navigate(donerAnalysis);
        }
    }
}
