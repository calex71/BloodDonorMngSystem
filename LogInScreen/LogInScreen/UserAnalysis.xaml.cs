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

        }
    }
}
