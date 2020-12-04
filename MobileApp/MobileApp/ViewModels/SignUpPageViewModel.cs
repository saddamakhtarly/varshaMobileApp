using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
  
    public class SignUpPageViewModel : NotifyModel
    {
        public INavigation Navigation { get; set; }
        
        public SignUpPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public Command SubmitClicked
        {
            get
            {
                return new Command(() =>
                {
                    if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && Password == ConfirmPassword)
                    {
                        User user = new User();
                        user.Email = Email;
                        user.Username = Email;
                        user.Mobile = Mobile;
                        user.Password = Password;
                        int resp = GlobleVariable.conn.Insert(user);
                        if (resp > 0)
                        {
                           
                            Navigation.PopAsync();
                        }
                        else 
                        {
                            Application.Current.MainPage.DisplayAlert("Message", "Data fail to save", "Ok");
                        }
                       
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Message", "Please enter valid Credential", "Ok");
                    }
                });
            }
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

        string _mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if (value != null)
                {
                    _mobile = value;
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
        string _confirmpassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmpassword;
            }
            set
            {
                if (value != null)
                {
                    _confirmpassword = value;
                    OnPropertyChanged();
                }
            }
        }
    }
 }
