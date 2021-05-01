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
    public class EditTermPage : ContentPage
    {
        private ListView _listView;
        private Entry _idEntry;
        private Entry _nameEntry;
        private Label _startDateLabel;
        private Label _endDateLabel;
        private DatePicker _startDate;
        private DatePicker _endDate;
        private Button _button;

        Term _term = new Term();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public EditTermPage()
        {
            this.Title = "Edit Term";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<Term>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _idEntry = new Entry();
            _idEntry.Placeholder = "ID";
            _idEntry.IsVisible = false;
            stackLayout.Children.Add(_idEntry);

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

            _button = new Button();
            _button.Text = "Update";
            _button.Clicked += _button_Clicked;
            stackLayout.Children.Add(_button);

            Content = stackLayout;

        }

        private async void _button_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            Term term = new Term()
            {
                Id = Convert.ToInt32(_idEntry.Text),
                Name = _nameEntry.Text,
                StartDate = _startDate.Date,
                EndDate = _endDate.Date
            };

            if (term.StartDate < term.EndDate)
            {
                db.Update(term);
                await DisplayAlert(null, term.Name + " Saved", "Ok");
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");

        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _term = (Term)e.SelectedItem;
            _idEntry.Text = _term.Id.ToString();
            _nameEntry.Text = _term.Name;
            _startDate.Date = Convert.ToDateTime(_term.StartDate);
            _endDate.Date = Convert.ToDateTime(_term.EndDate);
        }
    }
}