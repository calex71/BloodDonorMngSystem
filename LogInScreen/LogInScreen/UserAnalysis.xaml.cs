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
    /// Interaction logic for UserAnalysis.xaml
    /// </summary>
    public partial class UserAnalysis : UserControl
    {
        List<User> userList = new List<User>();
        List<Log> logList = new List<Log>();

        BloodDBEntities db = new BloodDBEntities("metadata=res://*/BloodDonorModel1.csdl|res://*/BloodDonorModel1.ssdl|res://*/BloodDonorModel1.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.200;initial catalog=BloodDB;persist security info=True;user id=blooddonor;password=password;MultipleActiveResultSets=True;App=EntityFramework'");

        enum AnalysisType
        {
            Summary,
            Count,
            Statistics
        }

        private AnalysisType analysisType = new AnalysisType();

        enum TableSelection
        {
            User,
            Log
        }

        private TableSelection tableSelection = new TableSelection();

        public UserAnalysis()
        {
            InitializeComponent();
        }

        private void CboAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAnalysisType.SelectedIndex > 0)
            {
                if (cboAnalysisType.SelectedIndex == 1)
                {
                    analysisType = AnalysisType.Summary;
                }
                if (cboAnalysisType.SelectedIndex == 2)
                {
                    analysisType = AnalysisType.Count;
                }
                if (cboAnalysisType.SelectedIndex == 3)
                {
                    analysisType = AnalysisType.Statistics;
                }
            }
        }

        private void CboChooseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboChooseTable.SelectedIndex > 0)
            {
                if (cboChooseTable.SelectedIndex == 1)
                {
                    tableSelection = TableSelection.User;
                }
                if (cboChooseTable.SelectedIndex == 2)
                {
                    tableSelection = TableSelection.Log;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var userRecord in db.Users)
            {
                userList.Add(userRecord);
            }
            foreach (var logRecord in db.Logs)
            {
                logList.Add(logRecord);
            }
        }

        private void BtnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            int recordCount = 0;
            string output = "";
            tbkAnalysisOutput.Text = "";

            if (analysisType == AnalysisType.Summary && tableSelection == TableSelection.User)
            {
                int level1CountSummary = 0;
                int level2CountSummary = 0;
                int level3CountSummary = 0;
                foreach (var item in userList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for the user whose " +
                        $"name is {item.Forename} {item.Surname}, with the username {item.Username}. This " +
                        $"user has access level {item.LevelID} which is {item.AccessLevel.JobRole} job role" + Environment.NewLine;

                    if (item.LevelID  == 1)
                    {
                        level1CountSummary++;
                    }
                    if (item.LevelID == 2)
                    {
                        level2CountSummary++;
                    }
                    if (item.LevelID == 3)
                    {
                        level3CountSummary++;
                    }
                }
                output = output + Environment.NewLine + $"Total users with level 1 is {level1CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with level 2 is {level2CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with level 3 is {level3CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total number of records = {recordCount}";

                tbkAnalysisOutput.Text = output;
            }
            if (analysisType == AnalysisType.Summary && tableSelection == TableSelection.Log)
            {
                foreach (var item in logList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for log created by {item.User.Forename} {item.User.Surname} with the u" +
                        $"ser ID is {item.User.UserID}. Log was created on {item.Date}. This log was registered for the {item.Catagory} event." + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total log entries = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Statistics && tableSelection == TableSelection.User)
            {
                int level1CountSummary = 0;
                int level2CountSummary = 0;
                int level3CountSummary = 0;
                foreach (var item in userList)
                {
                    recordCount++;
                    if (item.LevelID == 1)
                    {
                        level1CountSummary++;
                    }
                    if (item.LevelID == 2)
                    {
                        level2CountSummary++;
                    }
                    if (item.LevelID == 3)
                    {
                        level3CountSummary++;
                    }                   
                }
                output = output + Environment.NewLine + $"Total users with level 1, Office Clerical access is {level1CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with level 2, Clinic Staff access is {level2CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with level 3, System Administrator access is {level3CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total number of records = {recordCount}";

                tbkAnalysisOutput.Text = output;
            }

            // Add another for logs statistics once I've done more log categories
        }
    }
}
