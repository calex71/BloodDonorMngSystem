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
using System.Windows.Shapes;

namespace LogInScreen
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
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

        private void MnuShowGroups_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu option is checked");
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

        //click event for the manage donors submenu
        private void MnuManageDonorsMenu_Click(object sender, RoutedEventArgs e)
        {
            Donor donor = new Donor();           
            frmMain.Navigate(donor);
        }

        //Access level check to determin what the user sees and has access to.
        //Based on a check of their access LevelID.
        private void CheckUserAccess(User user)
        {        
            if (user.LevelID == 3)
            {
                mnuAdminMenu.Visibility = Visibility.Visible;
                mnuToolsMenu.Visibility = Visibility.Visible;               
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
        }
    }
}
