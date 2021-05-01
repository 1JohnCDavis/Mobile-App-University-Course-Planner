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
    public class DeleteAssessmentPage : ContentPage
    {
        private Entry _courseIdEntry;
        private ListView _listView;
        private Button _deleteButton;

        Assessment _assessment = new Assessment();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public DeleteAssessmentPage(Course chosenCourse)
        {
            this.Title = "Delete Assessment";

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

            _deleteButton = new Button();
            _deleteButton.Text = "Delete";
            _deleteButton.Clicked += _deleteButton_Clicked;
            stackLayout.Children.Add(_deleteButton);

            Content = stackLayout;
        }

        private async void _deleteButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<Assessment>().Delete(x => x.Id == _assessment.Id);
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _assessment = (Assessment)e.SelectedItem;
        }
    }
}