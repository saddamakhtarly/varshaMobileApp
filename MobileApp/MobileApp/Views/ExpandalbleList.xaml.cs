using MobileApp.Models;
using MobileApp.ViewModels;
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
    public partial class ExpandalbleList : ContentPage
    {
        public ExpandalbleList()
        {
            InitializeComponent();
            BindingContext = new ExpandableListViewModel();
        }
    }

    public class TestData : NotifyModel
    {
        public string Name { get; set; }
        bool _isVisibleButtons;
        public bool IsVisibleButtons
        {
            get
            {
                return _isVisibleButtons;
            }
            set
            {
                _isVisibleButtons = value;
                OnPropertyChanged();
            }
        }
    }
}