using FFImageLoading.Forms;
using MediaManager.Forms;
using MobileApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class RelativePage : ContentPage
    {
        
        public RelativePage()
        {
            InitializeComponent();

            //var height = Application.Current.MainPage.Height;
            //var width = Application.Current.MainPage.Width;
            //wv.HeightRequest = height;
            //wv.WidthRequest = width;
           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Message" , "Photo capture not supported","ok");
            }
            else
            {
                try
                {
                    if (await new GlobleFunction().RequestCameraPermission())
                    {
                        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        {
                            Name = "Test.jpg",
                            Directory = "MobileAppMedia"
                        });
                        if (file != null)
                        {
                            var image = new CachedImage()
                            {
                                Source = ImageSource.FromStream(() => file.GetStream())
                            };
                            image.Success += (s, ev) =>
                            {
                                var h = ev.ImageInformation.OriginalHeight;
                                var w = ev.ImageInformation.OriginalWidth;
                            };
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                img.Source = image.Source;
                            });

                            var resp = await new HttpRequest().SaveImage(new List<MediaFile> { file });
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            RazorPaymentData data = new RazorPaymentData();
            data.amount = 50000;
            data.currency = "INR";
            data.receipt = "SUDCYBSDC5125";
            data.payment_capture = 1;
            MessagingCenter.Send<RazorPaymentData>(data, "PayViaRazorPay");
        }
    }
}