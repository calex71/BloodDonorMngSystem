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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogInScreen
{
    /// <summary>
    /// Interaction logic for bdonor.xaml
    /// </summary>
    public partial class bdonor : Page
    {
        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel1.csdl|res://*/BloodDonorModel1.ssdl|res://*/BloodDonorModel1.msl;provider=System.Data.SqlClient;provider connection string=';data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");

        List<Donor> donors = new List<Donor>();
        Donor selectedDonor = new Donor();

        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dBOperation = new DBOperation();
        
        public bdonor()
        {
            InitializeComponent();
        }

        //Right click context menu to add new donor and show the stack panel with the data entry controls
        private void SubmnuAddNewDonor_Click(object sender, RoutedEventArgs e)
        {
            stkDonorDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.Add;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDonorList();
            lstDonorList.ItemsSource = donors;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            bool donorDataValidated = ValidateDonorInput();
            if (!donorDataValidated)
            {
                MessageBox.Show("Problem with data entered!!!!!! Please check the following :" + Environment.NewLine + "There are no empty fields." + Environment.NewLine + "Fields do not exceed 30 characters and street does not exceed 50." + Environment.NewLine + "Blood Group has been selected from the combo box.", "Invalid data", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            if (dBOperation == DBOperation.Add && donorDataValidated)
            {
                Donor donor = new Donor();
                donor.Forename = tbxDonorForename.Text.Trim();
                donor.Surname = tbxDonorSurname.Text.Trim();
                donor.Street = tbxDonorStreet.Text.Trim();
                donor.Town = tbxDonorTown.Text.Trim();
                donor.County = tbxDonorCounty.Text.Trim();
                donor.DonationDate = dtpDonationDate.SelectedDate.Value.Date;
                donor.BloodGroupID = cboBloodGroupID.SelectedIndex;
                int saveSuccess = SaveDonor(donor);
                if (saveSuccess == 1)
                {
                    MessageBox.Show("Donor record saved", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshDonorList();
                    ClearDonorListDetails();
                    stkDonorDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Donor record did not save", "Save to database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (dBOperation == DBOperation.Modify && donorDataValidated)
            {
                foreach (var donor in db.Donors.Where(t => t.DonorID == selectedDonor.DonorID))
                {
                    donor.Forename = tbxDonorForename.Text.Trim();
                    donor.Surname = tbxDonorSurname.Text.Trim();
                    donor.Street = tbxDonorStreet.Text.Trim();
                    donor.Town = tbxDonorTown.Text.Trim();
                    donor.County = tbxDonorCounty.Text.Trim();
                    donor.DonationDate = selectedDonor.DonationDate;
                    donor.BloodGroupID = selectedDonor.BloodGroupID;
                }
                int saveSuccess = db.SaveChanges();
                if (saveSuccess == 1)
                {
                    MessageBox.Show("Donor record modified", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshDonorList();
                    ClearDonorListDetails();
                    stkDonorDetails.Visibility = Visibility.Collapsed;
                }
            }
        }

        public int SaveDonor(Donor donor)
        {
            db.Entry(donor).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void RefreshDonorList()
        {
            lstDonorList.ItemsSource = donors;
            donors.Clear();
            foreach (var donor in db.Donors)
            {
                donors.Add(donor);
            }
            lstDonorList.Items.Refresh();            
        }

        private void ClearDonorListDetails()
        {
            tbxDonorForename.Text = "";
            tbxDonorSurname.Text = "";
            tbxDonorStreet.Text = "";
            tbxDonorTown.Text = "";
            tbxDonorCounty.Text = "";
            dtpDonationDate.SelectedDate = null;
            cboBloodGroupID.SelectedIndex = 0;
        }

        private void LstDonorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDonorList.SelectedIndex > 0)
            {
                selectedDonor = donors.ElementAt(lstDonorList.SelectedIndex);
                submnuModifySelectedDonor.IsEnabled = true;
                submnuDeleteSelectedDonor.IsEnabled = true;
                if (dBOperation == DBOperation.Add)
                {
                    ClearDonorListDetails();
                }                
                if (dBOperation == DBOperation.Modify)
                {
                    
                    tbxDonorForename.Text = selectedDonor.Forename;
                    tbxDonorSurname.Text = selectedDonor.Surname;
                    tbxDonorStreet.Text = selectedDonor.Street;
                    tbxDonorTown.Text = selectedDonor.Town;
                    tbxDonorCounty.Text = selectedDonor.County;
                    dtpDonationDate.SelectedDate = selectedDonor.DonationDate;
                    cboBloodGroupID.SelectedIndex = selectedDonor.BloodGroupID;
                }
            }           
        }

        private void SubmnuModifySelectedDonor_Click(object sender, RoutedEventArgs e)
        {
            stkDonorDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.Modify;
        }

        private void SubmnuDeleteSelectedDonor_Click(object sender, RoutedEventArgs e)
        {
            db.Donors.RemoveRange(db.Donors.Where(t => t.DonorID == selectedDonor.DonorID));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Donor record deleted", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshDonorList();
                ClearDonorListDetails();
                stkDonorDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem deleting donor recored", "Delete from database", MessageBoxButton.OK, MessageBoxImage.Warning);
                RefreshDonorList();
                ClearDonorListDetails();
            }            
        }

        public bool ValidateDonorInput()
        {
            bool validated = true;

            if (tbxDonorCounty.Text.Length == 0 || tbxDonorCounty.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxDonorForename.Text.Length == 0 || tbxDonorForename.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxDonorStreet.Text.Length == 0 || tbxDonorStreet.Text.Length > 50)
            {
                validated = false;
            }

            if (tbxDonorSurname.Text.Length == 0 || tbxDonorSurname.Text.Length > 30)
            {
                validated = false;
            }

            if (cboBloodGroupID.SelectedIndex < 1 || cboBloodGroupID.SelectedIndex > 8)
            {
                validated = false;
            }
            return validated;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearDonorListDetails();
            stkDonorDetails.Visibility = Visibility.Collapsed;
        }
    }

    
}
