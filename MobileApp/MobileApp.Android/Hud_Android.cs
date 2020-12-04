using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using MobileApp.Droid;
using MobileApp.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Hud_Android))]
namespace MobileApp.Droid
{
   
    public class Hud_Android : IHud
    {
        public Hud_Android()
        {
        }

        public void Show()
        {
            AndroidHUD.AndHUD.Shared.Show(Xamarin.Forms.Forms.Context);
        }

        public void Show(string message)
        {
            AndHUD.Shared.Show(Xamarin.Forms.Forms.Context, message);
        }

        public void Dismiss()
        {
            AndHUD.Shared.Dismiss(Xamarin.Forms.Forms.Context);
        }

    }
}