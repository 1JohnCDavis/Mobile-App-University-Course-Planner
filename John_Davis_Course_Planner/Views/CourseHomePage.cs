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
    public class CourseHomePage : ContentPage
    {
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        private Entry _chosenTerm;

        public CourseHomePage(Term chosenTerm)
        {
            this.Title = "Courses";

            StackLayout stackLayout = new StackLayout();

            _chosenTerm = new Entry();
            _chosenTerm.Text = chosenTerm.Id.ToString();
            _chosenTerm.IsVisible = false;
            stackLayout.Children.Add(_chosenTerm);

            Button addButton = new Button();
            addButton.Text = "Add Course";
            addButton.Clicked += AddButton_Clicked;
            stackLayout.Children.Add(addButton);

            Button viewButton = new Button();
            viewButton.Text = "View Course(s)";
            viewButton.Clicked += ViewButton_Clicked;
            stackLayout.Children.Add(viewButton);

            Button editButton = new Button();
            editButton.Text = "Edit Course";
            editButton.Clicked += EditButton_Clicked;
            stackLayout.Children.Add(editButton);

            Button deleteButton = new Button();
            deleteButton.Text = "Delete Course";
            deleteButton.Clicked += DeleteButton_Clicked;
            stackLayout.Children.Add(deleteButton);

            Content = stackLayout;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Term chosenTerm = new Term()
            {
                Id = Convert.ToInt32(_chosenTerm.Text)
            };

            await Navigation.PushAsync(new DeleteCoursePage(chosenTerm));
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            Term chosenTerm = new Term()
            {
                Id = Convert.ToInt32(_chosenTerm.Text)
            };

            await Navigation.PushAsync(new EditCoursePage(chosenTerm));
        }

        private async void ViewButton_Clicked(object sender, EventArgs e)
        {
            Term chosenTerm = new Term()
            {
                Id = Convert.ToInt32(_chosenTerm.Text)
            };

            await Navigation.PushAsync(new ViewCoursePage(chosenTerm));
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            Term chosenTerm = new Term()
            {
                Id = Convert.ToInt32(_chosenTerm.Text)
            };

            await Navigation.PushAsync(new AddCoursePage(chosenTerm));
        }
    }
}