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
        

        public admin()
        {
            InitializeComponent();
        }

        //click event to show the user details stack panel to add a new user
        private void SubmnuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
        }

        

        
    }
}
