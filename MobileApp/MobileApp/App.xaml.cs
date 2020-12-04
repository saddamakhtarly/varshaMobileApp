using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using MobileApp.Interfaces;
using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    public partial class App : Application
    {
        int timeOut = 20;
        bool isTimeStarted = false;
        public App()
        {
            InitializeComponent();

            MainPage = new RelativePage();
            GlobleVariable.conn = DependencyService.Get<ISqlite>().GetConnection();
            List<string> data = new List<string>() {"1","2" };
            var firstString = data.First();
            try
            {
                int a = 2;
                int b = 0;
                var v = a/b;
            }
            catch (Exception ex)
            {
                new GlobleFunction().SendCrashReport(ex);
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=5282184c-e4ba-45b4-8983-b6616c79bb0f",
                  typeof(Crashes));
        }

        protected override void OnSleep()
        {
            isTimeStarted = true;
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if(timeOut == 0)
                {
                    isTimeStarted = false;                   
                }
                else
                {
                    timeOut -= 1;
                }
              
                return isTimeStarted;
            });
        }

        protected override void OnResume()
        {
            if(timeOut == 0)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    MainPage = new NavigationPage(new LoginPage());
                });
                timeOut = 20;
                isTimeStarted = false;
            }
            else
            {
                timeOut = 20;
                isTimeStarted = false;
            }
        }
    }
}
