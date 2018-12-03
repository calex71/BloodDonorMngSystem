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
    /// Interaction logic for bdonor.xaml
    /// </summary>
    public partial class bdonor : Page
    {
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel.csdl|res://*/BloodDonorModel.ssdl|res://*/BloodDonorModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");

        List<Donor> donors = new List<Donor>();
        public bdonor()
        {
            InitializeComponent();
        }

        //Right click context menu to add new donor and show the stack panel with the data entry controls
        private void SubmnuAddNewDonor_Click(object sender, RoutedEventArgs e)
        {
            stkDonorDetails.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstDonorList.ItemsSource = donors;
            foreach (var donor in db.Donors)
            {
                donors.Add(donor);
            }
        }
    }

    
}
