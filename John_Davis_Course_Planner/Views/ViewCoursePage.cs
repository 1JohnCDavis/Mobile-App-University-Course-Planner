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
    public class ViewCoursePage : ContentPage
    {
        private ListView _listView;
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        Course chosenCourse = new Course();

        public ViewCoursePage(Term chosenTerm)
        {            
            this.Title = "View Course(s)";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();
            
            _listView = new ListView();
            _listView.ItemsSource = db.Query<Course>($"SELECT * FROM Course WHERE TermId = {chosenTerm.Id}");
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            Content = stackLayout;
        }
        
        private async void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            chosenCourse = (Course)e.SelectedItem;

            await Navigation.PushAsync(new CourseDetailsPage(chosenCourse));
        }
    }
}