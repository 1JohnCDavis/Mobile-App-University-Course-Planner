using John_Davis_Course_Planner.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace John_Davis_Course_Planner.Views
{
    public class AssessmentDetailsPage : ContentPage
    {
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        private Label _assessmentName;
        private Label _dueDate;
        private Label _notificationsEnabled;

        public AssessmentDetailsPage(Assessment chosenAssessment)
        {
            this.Title = "Assessment Details";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _assessmentName = new Label();
            _assessmentName.Text = "Type:  " + chosenAssessment.Name.ToString();
            stackLayout.Children.Add(_assessmentName);

            _dueDate = new Label();
            _dueDate.Text = "Due Date:  " + chosenAssessment.DueDate.ToString("MM/dd/yyyy");
            stackLayout.Children.Add(_dueDate);

            _notificationsEnabled = new Label();
            _notificationsEnabled.Text = "Notifications Enabled:  " + chosenAssessment.NotificationsEnabled.ToString();
            stackLayout.Children.Add(_notificationsEnabled);

            Content = stackLayout;
        }
    }
}