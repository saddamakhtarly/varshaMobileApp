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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using MobileApp.Renderer;
using MobileApp.Droid;

[assembly: ExportRenderer(typeof(LoginEntry), typeof(LoginEntryRenderer))]
namespace MobileApp.Droid
{
   // [Obsolete]
    public class LoginEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();

                //this line sets the bordercolor
                gd.SetColor(global::Android.Graphics.Color.Rgb(247, 247, 247));
                gd.SetCornerRadius(10);
                gd.SetStroke(1, global::Android.Graphics.Color.Rgb(192, 192, 194));

                this.Control.SetBackgroundDrawable(gd);
                Control.SetPadding(17, 17, 17, 17);

                //  this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagSigned | Android.Text.InputTypes.NumberFlagDecimal;
                //  Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.LightGray));
            }
        }
    }
}