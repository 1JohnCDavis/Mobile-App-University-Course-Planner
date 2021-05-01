using John_Davis_Course_Planner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace John_Davis_Course_Planner.Views
{
    public class AssessmentHomePage : ContentPage
    {
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        private Entry _chosenCourse;

        public AssessmentHomePage(Course chosenCourse)
        {
            this.Title = "Assessments";

            StackLayout stackLayout = new StackLayout();

            _chosenCourse = new Entry();
            _chosenCourse.Text = chosenCourse.Id.ToString();
            _chosenCourse.IsVisible = false;
            stackLayout.Children.Add(_chosenCourse);

            Button addButton = new Button();
            addButton.Text = "Add Assessment";
            addButton.Clicked += AddButton_Clicked;
            stackLayout.Children.Add(addButton);

            Button viewButton = new Button();
            viewButton.Text = "View Assessment(s)";
            viewButton.Clicked += ViewButton_Clicked;
            stackLayout.Children.Add(viewButton);

            Button editButton = new Button();
            editButton.Text = "Edit Assessment";
            editButton.Clicked += EditButton_Clicked;
            stackLayout.Children.Add(editButton);

            Button deleteButton = new Button();
            deleteButton.Text = "Delete Assessment";
            deleteButton.Clicked += DeleteButton_Clicked;
            stackLayout.Children.Add(deleteButton);

            Content = stackLayout;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Course chosenCourse = new Course()
            {
                Id = Convert.ToInt32(_chosenCourse.Text)
            };

            await Navigation.PushAsync(new DeleteAssessmentPage(chosenCourse));
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            Course chosenCourse = new Course()
            {
                Id = Convert.ToInt32(_chosenCourse.Text)
            };

            await Navigation.PushAsync(new EditAssessmentPage(chosenCourse));
        }

        private async void ViewButton_Clicked(object sender, EventArgs e)
        {
            Course chosenCourse = new Course()
            {
                Id = Convert.ToInt32(_chosenCourse.Text)
            };

            await Navigation.PushAsync(new ViewAssessmentPage(chosenCourse));
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            Course chosenCourse = new Course()
            {
                Id = Convert.ToInt32(_chosenCourse.Text)
            };

            await Navigation.PushAsync(new AddAssessmentPage(chosenCourse));
        }
    }
}