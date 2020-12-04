using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
   public class ResetPasswordPageViewModel:NotifyModel
    {
        public INavigation Navigation { get; set; }
        public ResetPasswordPageViewModel( INavigation navigation)
        {
            Navigation = navigation;
        }
        public Command SubmitClicked
        {
            get
            {
                return new Command(() =>
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                        Application.Current.MainPage.DisplayAlert("Message", "Success", "Ok");
                        Navigation.PopAsync();
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Message", "Please enter valid email", "Ok");
                    }
                });
            }
        }
        public Command BackbtnClicked
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PopAsync();
                });
            }
        }
        string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != null)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        

    }
}
