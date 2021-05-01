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
    public class DeleteCoursePage : ContentPage
    {
        private Entry _termIdEntry;
        private ListView _listView;
        private Button _button;

        Course _course = new Course();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public DeleteCoursePage(Term chosenTerm)
        {
            this.Title = "Delete Course";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _termIdEntry = new Entry();
            _termIdEntry.Text = chosenTerm.Id.ToString();
            _termIdEntry.Placeholder = "ID";
            _termIdEntry.IsVisible = false;
            stackLayout.Children.Add(_termIdEntry);

            _listView = new ListView();
            _listView.ItemsSource = db.Query<Course>($"SELECT * FROM Course WHERE TermId = {chosenTerm.Id}");
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _button = new Button();
            _button.Text = "Delete";
            _button.Clicked += _button_Clicked;
            stackLayout.Children.Add(_button);

            Content = stackLayout;
        }

        private async void _button_Clicked(object sender, EventArgs e)
        {
            //var db = new SQLiteConnection(_dbPath);
            //db.Table<Course>().Delete(x => x.Id == _course.Id);
            //await Navigation.PopAsync();

            var db = new SQLiteConnection(_dbPath);
            var query = db.Query<Assessment>($"SELECT * FROM Assessment WHERE CourseId = {_course.Id}");

            if (query.Count == 0)
            {
                db.Table<Course>().Delete(x => x.Id == _course.Id);
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Error", "This course has assessments associated with it.  Please delete all assessments before attempting to remove this course.", "Ok");
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _course = (Course)e.SelectedItem;
        }
    }
}