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
    public partial class CourseDetailPage : ContentPage
    {
        public CourseDetailPage(Lessons lessons)
        {
            InitializeComponent();
        
             BindingContext = new CourseDetailPageViewModel(lessons,Navigation);
        }
    }
}