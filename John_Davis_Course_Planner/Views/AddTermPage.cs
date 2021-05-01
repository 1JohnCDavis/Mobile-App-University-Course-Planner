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
    public class AddTermPage : ContentPage
    {
        private Entry _nameEntry;
        private Label _startDateLabel;
        private Label _endDateLabel;
        private DatePicker _startDate;
        private DatePicker _endDate;
        private Button _saveButton;
        
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public AddTermPage()
        {
            this.Title = "Add Term";

            StackLayout stackLayout = new StackLayout();

            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Term Name";
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

            _saveButton = new Button();
            _saveButton.Text = "Save";
            _saveButton.Clicked += _saveButton_Clicked;
            stackLayout.Children.Add(_saveButton);

            Content = stackLayout;
        }

        
        
        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Term>();

            var maxPk = db.Table<Term>().OrderByDescending(c => c.Id).FirstOrDefault();

            Term term = new Term()
            {
                
                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                Name = _nameEntry.Text,
                StartDate = _startDate.Date,
                EndDate = _endDate.Date
            };

            if (term.StartDate < term.EndDate)
            {
                db.Insert(term);
                await DisplayAlert(null, term.Name + " Saved", "Ok");
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            
        }
    }
}