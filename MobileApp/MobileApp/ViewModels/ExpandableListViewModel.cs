using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class ExpandableListViewModel : NotifyModel
    {
        public ExpandableListViewModel()
        {
            ListData = new ObservableCollection<TestData>(); ;
            ListData.Add(new TestData { Name = "Test name 1" });
            ListData.Add(new TestData { Name = "Test name 2" });
            ListData.Add(new TestData { Name = "Test name 3" });
            ListData.Add(new TestData { Name = "Test name 4" });
        }

        TestData _selectedListItem;
        public TestData SelectedListItem
        {
            get
            {
                return _selectedListItem;
            }
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (_selectedListItem != null)
                    {
                        _selectedListItem.IsVisibleButtons = false;
                        //OnPropertyChanged();
                    }
                    _selectedListItem = value;
                    if (_selectedListItem != null)
                    {
                        _selectedListItem.IsVisibleButtons = true;
                        // OnPropertyChanged();
                    }
                });
            }
        }

        public Command DeleteItem
        {
            get
            {
                return new Command((e) =>
                {
                    TestData selectedItem = e as TestData;
                    if (selectedItem != null)
                    {
                        int index = ListData.IndexOf(selectedItem);
                        ListData.RemoveAt(index);
                    }
                });
            }
        }

        ObservableCollection<TestData> _listData;
        public ObservableCollection<TestData> ListData
        {
            get
            {
                return _listData;
            }
            set
            {
                if (value != null)
                {
                    _listData = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
