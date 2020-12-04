using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaManager;
using Plugin.CurrentActivity;
using FFImageLoading.Forms.Platform;
using Com.Razorpay;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.Media;
using MobileApp.Models;
using Newtonsoft.Json;
using Org.Json;
using Xamarin.Forms;

namespace MobileApp.Droid
{
    [Activity(Label = "MobileApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,IPaymentResultWithDataListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossMediaManager.Current.Init();
            //CrossCurrentActivity.Current.Init(this);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            CachedImageRenderer.Init(true);
            LoadApplication(new App());
            MessagingCenter.Subscribe<RazorPaymentData>(this, "PayViaRazorPay", (value) =>
              {
                  PayNow("rzp_test_e3ZzGLMFaO4Nll", "gTN3CIuNHDN87x5bNF6ORlgT", value);
              });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnPaymentError(int p0, string p1, PaymentData p2)
        {
            //
        }

        public void OnPaymentSuccess(string p0, PaymentData p1)
        {
            //
        }

        public async void PayNow(string userKey,string userPassword,RazorPaymentData paymentData)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.razorpay.com/v1/orders"))
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{userKey}:{userPassword}");
                    var authKey = Convert.ToBase64String(plainTextBytes);
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {authKey}");

                    var jsonData = JsonConvert.SerializeObject(paymentData);
                    request.Content = new StringContent(jsonData);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    var resp = await response.Content.ReadAsStringAsync();
                    try
                    {
                        RazorOrderResp R_response = JsonConvert.DeserializeObject<RazorOrderResp>(resp);
                        if (!string.IsNullOrEmpty(R_response.id))
                        {
                            Checkout checkout = new Checkout();
                            checkout.SetKeyID(userKey);
                            try
                            {
                                JSONObject options = new JSONObject();
                                options.Put("name", "Merchant Name");
                                options.Put("description", "Reference No. #123456");
                                options.Put("image", "https://s3.amazonaws.com/rzp-mobile/images/rzp.png");
                                options.Put("order_id", $"{R_response.id}");
                                options.Put("currency", "INR");
                                options.Put("amount", paymentData.amount);
                                checkout.Open(this, options);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }
    }
}