using DBLibrary1;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel1.csdl|res://*/BloodDonorModel1.ssdl|res://*/BloodDonorModel1.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");
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
        //Logs are created and saved if the user logs in or if they fail to do so.
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            bool login = false;
            bool validatedUserData = false;
            string currentUser = tbxUsername.Text;
            string currentPassword = tbxPassword.Password;
            validatedUserData = ValidateUserInput(currentUser, currentPassword);
            if (validatedUserData)
            {
                validatedUser = GetUserRecord(currentUser, currentPassword);
                if (validatedUser.UserID > 0)
                {
                    CreateLogEntry("Login", "Logged in.", validatedUser.UserID, validatedUser.Username);
                    Dashboard dashboard = new Dashboard();
                    dashboard.user = validatedUser;
                    dashboard.Owner = this; //not sure I need this line?
                    dashboard.ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login datials do not exist, Please check and try again.", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }                

            }
            else
            {
                MessageBox.Show("Problem with username or password, check login details and try again", "Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        //Method to create log entry.
        private void CreateLogEntry(string catagory, string description, int userID, string userName)
        {
            string comment = $"{description} username = {userName}";
            Log log = new Log();
            log.UserID = userID;
            log.Catagory = catagory;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        //Method to save entry to log table
        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }


        //Method to make sure a username and password are not blank and do not exceed 30 characters
        private bool ValidateUserInput(string username, string password)
        {
            bool validated = true;
            if (username.Length == 0 || username.Length > 30)
            {
                validated = false;
            }
            foreach (char ch in username)
            {
                if (ch >= '0' && ch <= 9)
                {
                    validated = false;
                }
            }

            if (password.Length == 0 || password.Length > 30)
            {
                validated = false;
            }
            return validated;
        }

        private User GetUserRecord(string username, string password)
        {
            User validatedUser = new User();
            try
            {              
                foreach (var user in db.Users.Where(t => t.Username == username && t.Password == password))
                {
                    validatedUser = user;
                }
            }
            catch(EntityException ex)
            {
                MessageBox.Show("No connection to the SQL server, Closing application" + ex.InnerException, "Connect to SQL server", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                Environment.Exit(0);                
            }            
            return validatedUser;
        }
    }
}
