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
    public class AddAssessmentPage : ContentPage
    {
        private Entry _courseIdEntry;
        private Picker _namePicker;
        private Label _dueDateLabel;
        private DatePicker _dueDate;
        private Picker _notificationsPicker;

        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public AddAssessmentPage(Course chosenCourse)
        {
            this.Title = "Add Assessment";

            StackLayout stackLayout = new StackLayout();

            _courseIdEntry = new Entry();
            _courseIdEntry.Text = chosenCourse.Id.ToString();
            _courseIdEntry.Placeholder = "ID";
            _courseIdEntry.IsVisible = false;
            stackLayout.Children.Add(_courseIdEntry);

            _namePicker = new Picker();
            _namePicker.Title = "Assessment Type";
            _namePicker.Items.Add("Performance Assessment");
            _namePicker.Items.Add("Objective Assessment");
            stackLayout.Children.Add(_namePicker);

            _dueDateLabel = new Label();
            _dueDateLabel.Text = "Due Date";
            stackLayout.Children.Add(_dueDateLabel);

            _dueDate = new DatePicker();
            stackLayout.Children.Add(_dueDate);

            _notificationsPicker = new Picker();
            _notificationsPicker.Title = "Enable Notifications";
            _notificationsPicker.Items.Add("Yes");
            _notificationsPicker.Items.Add("No");
            stackLayout.Children.Add(_notificationsPicker);

            _saveButton = new Button();
            _saveButton.Text = "Save";
            _saveButton.Clicked += _saveButton_Clicked;
            stackLayout.Children.Add(_saveButton);

            Content = stackLayout;
        }

        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Assessment>();

            var maxPk = db.Table<Assessment>().OrderByDescending(c => c.Id).FirstOrDefault();

            Assessment assessment = new Assessment()
            {

                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                CourseId = Convert.ToInt32(_courseIdEntry.Text),
                Name = Convert.ToString(_namePicker.SelectedItem),
                DueDate = _dueDate.Date,
                NotificationsEnabled = Convert.ToString(_notificationsPicker.SelectedItem)
            };

            // Validation for entries
            if (!string.IsNullOrWhiteSpace(Convert.ToString(_namePicker.SelectedItem)) &&
                !string.IsNullOrWhiteSpace(Convert.ToString(_notificationsPicker.SelectedItem)))
            {
                db.Insert(assessment);
                await DisplayAlert(null, assessment.Name + " Saved", "Ok");
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Error", "Please ensure all fields are completed.", "Ok");
        }
    }
}