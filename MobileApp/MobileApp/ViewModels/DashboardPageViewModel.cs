using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class DashboardPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public DashboardPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PopulateLessons();
        }
        private async void PopulateLessons()
        {
            var data = await new HttpRequest().GetLesson();
            LessonsList = data.Lessons;
        }
        
        public Command BackClicked
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PopAsync();
                });
            }
        }

        List<Lessons> _lessonsList;
        public List<Lessons> LessonsList
        {
            get
            {
                return _lessonsList;
            }
            set
            {
                if (value != null)
                {
                    _lessonsList = value;
                    OnPropertyChanged();
                }
            }
        }
    } 
}
