using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = new DashboardPageViewModel(Navigation);
        }

        private void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Lessons lesson = e.Item as Lessons;
            Navigation.PushAsync(new CourseDetailPage(lesson));
        }
    }
}