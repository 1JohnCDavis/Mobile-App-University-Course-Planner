using John_Davis_Course_Planner.Models;
using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace John_Davis_Course_Planner.Views
{
    public class HomePage : ContentPage
    {
        private bool pushNotification = true;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        
        public HomePage()
        {
            DefaultData();

            this.Title = "Student Portal";

            StackLayout stackLayout = new StackLayout();

            Button addButton = new Button();
            addButton.Text = "Add Term";
            addButton.Clicked += AddButton_Clicked;
            stackLayout.Children.Add(addButton);

            Button viewButton = new Button();
            viewButton.Text = "View Term(s)";
            viewButton.Clicked += ViewButton_Clicked;
            stackLayout.Children.Add(viewButton);

            Button editButton = new Button();
            editButton.Text = "Edit Term";
            editButton.Clicked += EditButton_Clicked;
            stackLayout.Children.Add(editButton);

            Button deleteButton = new Button();
            deleteButton.Text = "Delete Term";
            deleteButton.Clicked += DeleteButton_Clicked;
            stackLayout.Children.Add(deleteButton);

            Content = stackLayout;
        }

        private void DefaultData() 
        {
            var db = new SQLiteConnection(_dbPath);

            db.CreateTable<Term>();
            db.CreateTable<Course>();
            db.CreateTable<Assessment>();

            var maxPk = db.Table<Term>().OrderByDescending(c => c.Id).FirstOrDefault();

            var termList = db.Table<Term>().ToList();
            var courseList = db.Table<Course>().ToList();
            var assessmentList = db.Table<Assessment>().ToList();

            if (!termList.Any())
            {
                var defaultTerm = new Term();
                defaultTerm.Id = (maxPk == null ? 1 : maxPk.Id + 1);
                defaultTerm.Name = "Evaluation Term";
                defaultTerm.StartDate = new DateTime(2025, 06, 09);
                defaultTerm.EndDate = new DateTime(2025, 09, 06);
                db.Insert(defaultTerm);
                termList.Add(defaultTerm);

                var defaultCourse = new Course();
                defaultCourse.Id = (maxPk == null ? 1 : maxPk.Id + 1);
                defaultCourse.Name = "Evaluation Course";
                defaultCourse.StartDate = new DateTime(2025, 06, 09);
                defaultCourse.EndDate = new DateTime(2025, 09, 06);
                defaultCourse.Status = "In Progress";
                defaultCourse.InstructorName = "John Davis";
                defaultCourse.InstructorPhone = "555-555-5555";
                defaultCourse.InstructorEmail = "jda1128@wgu.edu";
                defaultCourse.NotificationsEnabled = "Yes";
                defaultCourse.Notes = "No notes";
                defaultCourse.TermId = defaultTerm.Id;
                db.Insert(defaultCourse);

                var defaultPA = new Assessment();
                defaultPA.Id = (maxPk == null ? 1 : maxPk.Id + 1);
                defaultPA.Name = "Performance Assessment";
                defaultPA.DueDate = new DateTime(2025, 09, 06);
                defaultPA.NotificationsEnabled = "Yes";
                defaultPA.CourseId = defaultCourse.Id;
                db.Insert(defaultPA);

                var maxPk1 = db.Table<Term>().OrderByDescending(c => c.Id).FirstOrDefault();

                var defaultOA = new Assessment();
                defaultOA.Id = maxPk1.Id + 1;
                defaultOA.Name = "Objective Assessment";
                defaultOA.DueDate = new DateTime(2025, 09, 06);
                defaultOA.NotificationsEnabled = "Yes";
                defaultOA.CourseId = defaultCourse.Id;
                db.Insert(defaultOA);
            }

            if (pushNotification == true)
            {
                pushNotification = false;
                int courseId = 0;
                foreach (Course course in courseList)
                {
                    courseId++;
                    if (course.NotificationsEnabled == "Yes")
                    {
                        if (course.StartDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{course.Name} starts today.", courseId);
                        if (course.EndDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{course.Name} ends today.", courseId);
                    }
                }

                int assessmentId = courseId;
                foreach (Assessment assessment in assessmentList)
                {
                    assessmentId++;
                    if (assessment.NotificationsEnabled == "Yes")
                    {
                        if (assessment.DueDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{assessment.Name} is due today.", assessmentId);
                    }
                }
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteTermPage());
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage());
        }

        private async void ViewButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewTermPage());
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermPage());
        }
    }
}