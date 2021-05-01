using John_Davis_Course_Planner.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace John_Davis_Course_Planner.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}