using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TasksManager.WpfClient.Behavior;
using TasksManager.WpfClient.Data;
using TasksManager.WpfClient.Helpers;

namespace RecepiesClient.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            this.LoginRegisterVM = new LoginRegisterViewModel();
        }

        public LoginRegisterViewModel LoginRegisterVM { get; set; }
    }
}
