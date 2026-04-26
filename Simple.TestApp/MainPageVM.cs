using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.TestApp
{
    public partial class MainPageVM : ObservableObject
    {
        [ObservableProperty]
        private string _text = "Notification";
        
        [ObservableProperty]
        private bool _show = false;

        [ObservableProperty]
        private bool _cancel = false;

        [RelayCommand]
        public void ShowNotification()
        {
            Show = true;
        }

        [RelayCommand]
        public void CancelNotification()
        {
            Cancel = true;
        }
    }
}
