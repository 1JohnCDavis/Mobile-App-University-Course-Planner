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
    public class DeleteTermPage : ContentPage
    {
        private ListView _listView;
        private Button _button;

        Term _term = new Term();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

        public DeleteTermPage()
        {
            this.Title = "Delete Term";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<Term>().OrderBy(x => x.Name).ToList();
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
            var db = new SQLiteConnection(_dbPath);
            var query = db.Query<Course>($"SELECT * FROM Course WHERE TermId = {_term.Id}");

            if (query.Count == 0)
            {
                db.Table<Term>().Delete(x => x.Id == _term.Id);
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Error", "This term has courses associated with it.  Please delete all courses before attempting to remove this term.", "Ok");
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _term = (Term)e.SelectedItem;
        }
    }
}