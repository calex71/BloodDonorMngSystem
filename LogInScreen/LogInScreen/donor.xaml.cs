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
    /// Interaction logic for donor.xaml
    /// </summary>
    public partial class donor : Page
    {
        public donor()
        {
            InitializeComponent();
        }

        //Right click context menu to add new donor and show the stack panel with the data entry controls
        private void SubmnuAddNewDonor_Click(object sender, RoutedEventArgs e)
        {
            stkDonorDetails.Visibility = Visibility.Visible;
        }
    }
}
