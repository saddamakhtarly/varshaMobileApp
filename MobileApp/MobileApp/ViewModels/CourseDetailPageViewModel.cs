using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace MobileApp.ViewModels
{
   public class CourseDetailPageViewModel:NotifyModel
    {
        public int Id { get; set; }
        public INavigation Navigation { get; set; }

        public CourseDetailPageViewModel(Lessons lessons, INavigation navigation)
        {
            Navigation = navigation;
            Id = lessons.Id;
            LessonName = lessons.LessonName;
            Name = lessons.Name;
            PopulateMusiclist();
          //  musicList = GetMusics();
        }

        private async void PopulateMusiclist()
        {
            var data = await new HttpRequest().GetMusic(Id);
            MusicList = data.Musics;
        }
        private List<Course> GetMusics()
        {
            return new List<Course>
            {
                 new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
               new Course
               {
                    Name= "Ancient",
                    Time = "01:38",
               },
               new Course
               {
                    Name= "News Room News",
                    Time ="01:38",
               },
              new Course
               {
                    Name= "Bro Time",
                    Time = "01:38",
               },
               new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
              new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
               new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
                new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
               new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
                new Course
               {
                    Name= "Cheryl Porter",
                    Time = "01:38",
               },
            };
        }

        List<Music> musicList;
        public List<Music> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged();
            }
        }

        public Command PlayClicked
        {
            get
            {
                return new Command(async() =>
                {
                    var scannerPage = new ZXingScannerPage() { Title="Scan Code" };
                    await Navigation.PushAsync(scannerPage);
                    scannerPage.OnScanResult += ScanResult;

                    //Application.Current.MainPage.Navigation.PopAsync();
                    //bool cameraResp = await new GlobleFunction().RequestCameraPermission();
                    //if (cameraResp)
                    //{
                    //    var scannerPage = new ZXingScannerPage();
                    //    await Navigation.PushAsync(scannerPage);
                    //    scannerPage.OnScanResult += ScanResult;
                    //}
                });
            }
        }

        private async void ScanResult(Result result)
        {
            await Navigation.PopAsync();
            var scanData = result.Text;
        }

        public Command BackClicked
        {
            get
            {
                return new Command(() =>
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                });
            }
        }
       
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        string _lesson;

     
        public string LessonName
        {
            get
            {
                return _lesson;
            }
            set
            {
                if (value != null)
                {
                    _lesson = value;
                    OnPropertyChanged();
                }
            }
        }

        Music _selectedMusic;
        public Music SelectedMusic
        {
            get
            {
                return _selectedMusic;
            }
            set
            {
                if (value != null)
                {
                    _selectedMusic = value;
                    Navigation.PushAsync(new PlayerPage(value, MusicList));
                    OnPropertyChanged();
                }
            }
        }
    }
}
