﻿using DBLibrary;
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
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel.csdl|res://*/BloodDonorModel.ssdl|res://*/BloodDonorModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");


        List<User> users = new List<User>();
        List<Log> logs = new List<Log>();
        User selectedUser = new User();

        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dBOperation = new DBOperation();

        public admin()
        {
            InitializeComponent();
        }

        //click event to show the user details stack panel to add a new user
        private void SubmnuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.Add;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
            lstLogList.ItemsSource = logs;            
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (dBOperation == DBOperation.Add)
            {
                User user = new User();
                user.Username = tbxUsername.Text.Trim();
                user.Password = tbxPassword.Text.Trim();
                user.Forename = tbxForename.Text.Trim();
                user.Surname = tbxSurname.Text.Trim();
                user.LevelID = cboAccessLevel.SelectedIndex;
                int saveSuccess = SaveUser(user);
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User record saved successfully", "Save User Record", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("User record has not been saved", "Save User Record", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (dBOperation == DBOperation.Modify)
            {
                foreach (var user in db.Users.Where(t => t.UserID == selectedUser.UserID))
                {
                    user.Username = tbxUsername.Text.Trim();
                    user.Password = tbxPassword.Text.Trim();
                    user.Forename = tbxForename.Text.Trim();
                    user.Surname = tbxSurname.Text.Trim();
                    user.LevelID = cboAccessLevel.SelectedIndex;
                }
                int saveSuccess = db.SaveChanges();
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User record has been updated", "Save User Record", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserDetails.Visibility = Visibility.Collapsed;
                }
            }
        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        // Refreshes the user list when application loads and is also called when a new record is added by pressing the OK button.
        private void RefreshUserList()
        {
            lstUserList.ItemsSource = users;
            users.Clear();
            foreach (var user in db.Users)
            {
                users.Add(user);
            }
            lstUserList.Items.Refresh();
        }

        private void ClearUserDetails()
        {
            tbxUsername.Text = "";
            tbxPassword.Text = "";
            tbxForename.Text = "";
            tbxSurname.Text = "";
            cboAccessLevel.SelectedIndex = 0;
        }

        private void LstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUserList.SelectedIndex > 0)
            {
                selectedUser = users.ElementAt(lstUserList.SelectedIndex);
               
                    submnuModifySelectedUser.IsEnabled = true;
                    submnuDeleteSelectedUser.IsEnabled = true;
                    tbxUsername.Text = selectedUser.Username;
                    tbxPassword.Text = selectedUser.Password;
                    tbxForename.Text = selectedUser.Forename;
                    tbxSurname.Text = selectedUser.Surname;
                    cboAccessLevel.SelectedIndex = selectedUser.LevelID;
                
            }
            
        }

        private void SubmnuModifySelectedUser_Click(object sender, RoutedEventArgs e)
        {         
            stkUserDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.Modify;
        }

        private void SubmnuDeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            db.Users.RemoveRange(db.Users.Where(t => t.UserID == selectedUser.UserID));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("User record has been deleted", "Delete User Record", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                RefreshUserList();
                ClearUserDetails();
                stkUserDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting user record", "Delete User Record", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
