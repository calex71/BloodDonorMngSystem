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
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel.csdl|res://*/BloodDonorModel.ssdl|res://*/BloodDonorModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");


        List<User> users = new List<User>();
        List<Log> logs = new List<Log>();


        public admin()
        {
            InitializeComponent();
        }

        //click event to show the user details stack panel to add a new user
        private void SubmnuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {           
            lstUserList.ItemsSource = users;
            lstLogList.ItemsSource = logs;
            foreach (var user in db.Users)
            {
                users.Add(user);                
            }
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }
    }
}
