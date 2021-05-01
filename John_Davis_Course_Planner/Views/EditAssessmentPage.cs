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
    public class EditAssessmentPage : ContentPage
    {
        private Entry _courseIdEntry;
        private ListView _listView;
        private Entry _idEntry;
        private Picker _namePicker;
        private Label _dueDateLabel;
        private DatePicker _dueDate;
        private Picker _notificationsPicker;

        private Button _updateButton;

        Assessment _assessment = new Assessment();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public EditAssessmentPage(Course chosenCourse)
        {
            this.Title = "Edit Assessment";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _courseIdEntry = new Entry();
            _courseIdEntry.Text = chosenCourse.Id.ToString();
            _courseIdEntry.Placeholder = "ID";
            _courseIdEntry.IsVisible = false;
            stackLayout.Children.Add(_courseIdEntry);

            _listView = new ListView();
            _listView.ItemsSource = db.Query<Assessment>($"SELECT * FROM Assessment WHERE CourseId = {chosenCourse.Id}");
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _idEntry = new Entry();
            _idEntry.Placeholder = "ID";
            _idEntry.IsVisible = false;
            stackLayout.Children.Add(_idEntry);

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

            _updateButton = new Button();
            _updateButton.Text = "Update";
            _updateButton.Clicked += _updateButton_Clicked;
            stackLayout.Children.Add(_updateButton);

            Content = stackLayout;
        }

        private async void _updateButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            Assessment assessment = new Assessment()
            {
                Id = Convert.ToInt32(_idEntry.Text),
                CourseId = Convert.ToInt32(_courseIdEntry.Text),
                Name = Convert.ToString(_namePicker.SelectedItem),
                DueDate = _dueDate.Date,
                NotificationsEnabled = Convert.ToString(_notificationsPicker.SelectedItem)
            };

            // Validation for entries
            if (!string.IsNullOrWhiteSpace(Convert.ToString(_namePicker.SelectedItem)) &&
                !string.IsNullOrWhiteSpace(Convert.ToString(_notificationsPicker.SelectedItem)))
            {
                db.Update(assessment);
                await DisplayAlert(null, assessment.Name + " Saved", "Ok");
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Error", "Please ensure all fields are completed.", "Ok");
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _assessment = (Assessment)e.SelectedItem;
            _courseIdEntry.Text = _assessment.CourseId.ToString();
            _idEntry.Text = _assessment.Id.ToString();
            _namePicker.SelectedItem = _assessment.Name;
            _dueDate.Date = Convert.ToDateTime(_assessment.DueDate);
            _notificationsPicker.SelectedItem = _assessment.NotificationsEnabled;
        }
    }
}