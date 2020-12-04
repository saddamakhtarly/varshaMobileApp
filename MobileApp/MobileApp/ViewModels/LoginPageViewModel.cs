using MobileApp.Interfaces;
using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
   public class LoginPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        public LoginPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public Command LoginClicked
        {
            get
            {
                return new Command(async() =>
                {
                    if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                    {
                        DependencyService.Get<IHud>().Show("Logging In.");
                        LoginRequest rqst = new LoginRequest();
                        rqst.Username = Username;
                        rqst.Password = Password;
                        if(await new HttpRequest().LoginUser(rqst))
                        {
                            DependencyService.Get<IHud>().Dismiss();
                            await Navigation.PushAsync(new DashboardPage());
                        }
                        else
                        {
                            DependencyService.Get<IHud>().Dismiss();
                            await Application.Current.MainPage.DisplayAlert("Message", "Invalid Login", "Ok");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Message", "Please provide valid credential", "Ok");
                    }

                    //==============================
                    //GlobleVariable.conn.Execute("DELETE FROM User WHERE Id = ")
                });
            }
        }
        public Command SignupClicked 
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new SignUpPage());
                });
                
            }
        }
        public Command ForgotPasswordClicked
        {
            get
            {
                return new Command(() =>
                {
                   Navigation.PushAsync(new ResetPasswordPage());
                });

            }
        }
        string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != null)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != null)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
