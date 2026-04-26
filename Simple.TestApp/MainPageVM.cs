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
        private bool _show = false;

        [ObservableProperty]
        private bool _dismiss = false;

        [RelayCommand]
        public void ShowNotification()
        {
            Show = true;
        }

        [RelayCommand]
        public void CancelNotification()
        {
            Dismiss = true;
        }
    }
}
