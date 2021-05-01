using John_Davis_Course_Planner.ViewModels;
using John_Davis_Course_Planner.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace John_Davis_Course_Planner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
