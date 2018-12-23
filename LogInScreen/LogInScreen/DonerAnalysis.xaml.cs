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
    /// Interaction logic for DonerAnalysis.xaml
    /// </summary>
    public partial class DonerAnalysis : UserControl
    {
        List<Donor> donorList = new List<Donor>();

        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel1.csdl|res://*/BloodDonorModel1.ssdl|res://*/BloodDonorModel1.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");

        enum AnalysisType
        {
            Summary,
            Statistics
        }

        private AnalysisType analysisType = new AnalysisType();

        enum TableSelection
        {
            Donor          
        }

        private TableSelection tableSelection = new TableSelection();

        public DonerAnalysis()
        {
            InitializeComponent();
        }

        private void CboDonorAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboDonorAnalysisType.SelectedIndex > 0)
            {
                if (cboDonorAnalysisType.SelectedIndex == 1)
                {
                    analysisType = AnalysisType.Summary;
                }
                if (cboDonorAnalysisType.SelectedIndex == 2)
                {
                    analysisType = AnalysisType.Statistics;
                }
            }
        }

        private void CboChooseDonorTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboChooseDonorTable.SelectedIndex > 0)
            {
                if (cboChooseDonorTable.SelectedIndex == 1)
                {
                    tableSelection = TableSelection.Donor;
                }               
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var donorRecord in db.Donors)
            {
                donorList.Add(donorRecord);
            }
        }

        private void BtnAnalyseDonors_Click(object sender, RoutedEventArgs e)
        {
            int recordCount = 0;
            string output = "";
            tbkDonorAnalysisOutput.Text = "";

            if(analysisType == AnalysisType.Summary && tableSelection == TableSelection.Donor)
            {
                foreach (var item in donorList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for the donor whose " +
                       $"name is {item.Forename} {item.Surname}, with the blood type {item.BloodGroup}. This " +
                       $"donor is from {item.Town} which is in County {item.County}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total number of donor records = {recordCount}";

                tbkDonorAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Statistics && tableSelection == TableSelection.Donor)
            {
                int group1CountSummary = 0;
                int group2CountSummary = 0;
                int group3CountSummary = 0;
                int group4CountSummary = 0;
                int group5CountSummary = 0;
                int group6CountSummary = 0;
                int group7CountSummary = 0;
                int group8CountSummary = 0;

                foreach (var item in donorList)
                {
                    recordCount++;
                    if (item.BloodGroupID == 1)
                    {
                        group1CountSummary++;
                    }
                    if (item.BloodGroupID == 2)
                    {
                        group2CountSummary++;
                    }
                    if (item.BloodGroupID == 3)
                    {
                        group3CountSummary++;
                    }
                    if (item.BloodGroupID == 4)
                    {
                        group4CountSummary++;
                    }
                    if (item.BloodGroupID == 5)
                    {
                        group5CountSummary++;
                    }
                    if (item.BloodGroupID == 6)
                    {
                        group6CountSummary++;
                    }
                    if (item.BloodGroupID == 7)
                    {
                        group7CountSummary++;
                    }
                    if (item.BloodGroupID == 8)
                    {
                        group8CountSummary++;
                    }
                }
                output = output + Environment.NewLine + $"Total donors with blood type O positive is {group1CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type O negative is {group2CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type A positive is {group3CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type A negative is {group4CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type B positive is {group5CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type B negative is {group6CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type AB positive is {group7CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total donors with blood type AB negative is {group8CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total number of donors = {recordCount}";

                tbkDonorAnalysisOutput.Text = output;
            }
        }

        
    }
}
