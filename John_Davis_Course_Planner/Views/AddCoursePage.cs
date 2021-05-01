﻿using John_Davis_Course_Planner.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace John_Davis_Course_Planner.Views
{
    public class AddCoursePage : ContentPage
    {
        private Entry _termIdEntry;
        private Entry _nameEntry;
        private Label _startDateLabel;
        private Label _endDateLabel;
        private DatePicker _startDate;
        private DatePicker _endDate;
        private Picker _status;
        private Entry _instuctorNameEntry;
        private Entry _instructorPhoneEntry;
        private Entry _instructorEmailEntry;
        private Entry _notesEntry;
        private Picker _notificationsPicker;

        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        
        public AddCoursePage(Term chosenTerm)
        {
            this.Title = "Add Course";

            StackLayout stackLayout = new StackLayout();

            _termIdEntry = new Entry();
            _termIdEntry.Text = chosenTerm.Id.ToString();
            _termIdEntry.Placeholder = "ID";
            _termIdEntry.IsVisible = false;
            stackLayout.Children.Add(_termIdEntry);

            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Course Name";
            stackLayout.Children.Add(_nameEntry);

            _startDateLabel = new Label();
            _startDateLabel.Text = "Start Date";
            stackLayout.Children.Add(_startDateLabel);

            _startDate = new DatePicker();
            stackLayout.Children.Add(_startDate);

            _endDateLabel = new Label();
            _endDateLabel.Text = "End Date";
            stackLayout.Children.Add(_endDateLabel);

            _endDate = new DatePicker();
            stackLayout.Children.Add(_endDate);

            _status = new Picker();
            _status.Title = "Status";
            _status.Items.Add("In Progress");
            _status.Items.Add("Completed");
            stackLayout.Children.Add(_status);

            _instuctorNameEntry = new Entry();
            _instuctorNameEntry.Keyboard = Keyboard.Text;
            _instuctorNameEntry.Placeholder = "Instructor Name";
            stackLayout.Children.Add(_instuctorNameEntry);

            _instructorPhoneEntry = new Entry();
            _instructorPhoneEntry.Keyboard = Keyboard.Text;
            _instructorPhoneEntry.Placeholder = "Instructor Phone Number";
            stackLayout.Children.Add(_instructorPhoneEntry);

            _instructorEmailEntry = new Entry();
            _instructorEmailEntry.Keyboard = Keyboard.Text;
            _instructorEmailEntry.Placeholder = "Instructor Email";
            stackLayout.Children.Add(_instructorEmailEntry);

            _notesEntry = new Entry();
            _notesEntry.Keyboard = Keyboard.Text;
            _notesEntry.Placeholder = "Notes";
            stackLayout.Children.Add(_notesEntry);

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
            db.CreateTable<Course>();

            var maxPk = db.Table<Course>().OrderByDescending(c => c.Id).FirstOrDefault();

            Course course = new Course()
            {

                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                TermId = Convert.ToInt32(_termIdEntry.Text),
                Name = _nameEntry.Text,
                StartDate = _startDate.Date,
                EndDate = _endDate.Date,
                Status = Convert.ToString(_status.SelectedItem),
                InstructorName = _instuctorNameEntry.Text,
                InstructorPhone = _instructorPhoneEntry.Text,
                InstructorEmail = _instructorEmailEntry.Text,
                Notes = _notesEntry.Text,
                NotificationsEnabled = Convert.ToString(_notificationsPicker.SelectedItem)
            };

            // Validation for entries
            if (!string.IsNullOrWhiteSpace(_nameEntry.Text) &&
                !string.IsNullOrWhiteSpace(_instuctorNameEntry.Text) &&
                !string.IsNullOrWhiteSpace(Convert.ToString(_status.SelectedItem)) &&
                !string.IsNullOrWhiteSpace(_instructorPhoneEntry.Text) &&
                !string.IsNullOrWhiteSpace(_instructorEmailEntry.Text) &&
                /*!string.IsNullOrWhiteSpace(_notesEntry.Text) &&*/
                !string.IsNullOrWhiteSpace(Convert.ToString(_notificationsPicker.SelectedItem)))
            {
                if (_notesEntry.Text == null)
                {
                    course.Notes = "";
                }

                if (course.StartDate < course.EndDate)
                {
                    db.Insert(course);
                    await DisplayAlert(null, course.Name + " Saved", "Ok");
                    await Navigation.PopAsync();
                }
                else await DisplayAlert("Error", "Please ensure start date is before end date.", "Ok");
            }
            else await DisplayAlert("Error", "Please ensure all fields are completed.", "Ok");
        }
    }
}
