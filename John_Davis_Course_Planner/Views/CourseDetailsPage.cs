using John_Davis_Course_Planner.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace John_Davis_Course_Planner.Views
{
    public class CourseDetailsPage : ContentPage
    {
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        private Label _courseName;
        private Label _startDate;
        private Label _endDate;
        private Label _status;
        private Label _instructorName;
        private Label _instructorPhone;
        private Label _instructorEmail;
        private Label _notes;
        private Label _notificationsEnabled;

        private Button _assessmentsButton;
        private Button _shareButton;

        private Entry _chosenCourse;

        public CourseDetailsPage(Course chosenCourse)
        {
            this.Title = "Course Details";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _chosenCourse = new Entry();
            _chosenCourse.Text = chosenCourse.Id.ToString();
            _chosenCourse.IsVisible = false;
            stackLayout.Children.Add(_chosenCourse);

            _courseName = new Label();
            _courseName.Text = "Name:  " + chosenCourse.Name.ToString();
            stackLayout.Children.Add(_courseName);

            _startDate = new Label();
            _startDate.Text = "Start Date:  " + chosenCourse.StartDate.ToString("MM/dd/yyyy");
            stackLayout.Children.Add(_startDate);

            _endDate = new Label();
            _endDate.Text = "End Date:  " + chosenCourse.EndDate.ToString("MM/dd/yyyy");
            stackLayout.Children.Add(_endDate);

            _status = new Label();
            _status.Text = "Status:  " + chosenCourse.Status.ToString();
            stackLayout.Children.Add(_status);

            _instructorName = new Label();
            _instructorName.Text = "Instructor Name:  " + chosenCourse.InstructorName.ToString();
            stackLayout.Children.Add(_instructorName);

            _instructorPhone = new Label();
            _instructorPhone.Text = "Instructor Phone:  " + chosenCourse.InstructorPhone.ToString();
            stackLayout.Children.Add(_instructorPhone);

            _instructorEmail = new Label();
            _instructorEmail.Text = "Instructor Email:  " + chosenCourse.InstructorEmail.ToString();
            stackLayout.Children.Add(_instructorEmail);

            _notes = new Label();
            _notes.Text = "Notes:  " + chosenCourse.Notes.ToString();
            stackLayout.Children.Add(_notes);

            _notificationsEnabled = new Label();
            _notificationsEnabled.Text = "Notifications Enabled:  " + chosenCourse.NotificationsEnabled.ToString();
            stackLayout.Children.Add(_notificationsEnabled);

            _assessmentsButton = new Button();
            _assessmentsButton.Text = "Assessments";
            _assessmentsButton.Clicked += _assessmentsButton_Clicked;
            stackLayout.Children.Add(_assessmentsButton);

            _shareButton = new Button();
            _shareButton.Text = "Share Notes";
            _shareButton.Clicked += _shareButton_Clicked;
            stackLayout.Children.Add(_shareButton);

            Content = stackLayout;
        }

        private async void _shareButton_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = _notes.Text,
                Title = "Share notes from this course"
            });
        }

        private async void _assessmentsButton_Clicked(object sender, EventArgs e)
        {
            Course chosenCourse = new Course()
            {
                Id = Convert.ToInt32(_chosenCourse.Text)
            };

            await Navigation.PushAsync(new AssessmentHomePage(chosenCourse));
        }
    }  
}
