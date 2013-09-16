namespace RecepiesClient.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using RecepiesClient.Behavior;
    using RecepiesClient.Data;
    using RecepiesClient.Helpers;

    public class AppViewModel : ViewModelBase
    {
        private IPageViewModel currentViewModel;
        private ICommand changeViewModelCommand;
        private bool loggedInUser = false;
        private ICommand logoutCommand;

        public string Username { get; set; }

        public LoginRegisterFormViewModel LoginRegisterVM { get; set; }

        public RecipesViewModel RecipeVM { get; set; }

        public List<IPageViewModel> ViewModels { get; set; }

        public bool LoggedInUser
        {
            get
            {
                return this.loggedInUser;
            }
            set
            {
                this.loggedInUser = value;
                this.OnPropertyChanged("LoggedInUser");
            }
        }

        public ICommand Logout
        {
            get
            {
                if (this.logoutCommand == null)
                {
                    this.logoutCommand = new RelayCommand(this.HandleLogoutCommand);
                }

                return this.logoutCommand;
            }
        }

        private void HandleLogoutCommand(object parameter)
        {
            bool isUserLoggedOut = DataPersister.LogoutUser();
            if (isUserLoggedOut)
            {
                this.Username = "";
                this.LoggedInUser = false;
                this.CurrentViewModel = this.LoginRegisterVM;
                this.HandleChangeViewModelCommand(this.LoginRegisterVM);
            }
        }

        public IPageViewModel CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        public ICommand ChangeViewModel
        {
            get
            {
                if (this.changeViewModelCommand == null)
                {
                    this.changeViewModelCommand =
                        new RelayCommand(this.HandleChangeViewModelCommand);
                }

                return this.changeViewModelCommand;
            }
        }

        protected void HandleChangeViewModelCommand(object parameter)
        {
            var newCurrentViewModel = parameter as IPageViewModel;
            this.CurrentViewModel = newCurrentViewModel;
        }

        public AppViewModel()
        {
            this.ViewModels = new List<IPageViewModel>();
            var loginVM = new LoginRegisterFormViewModel();
            loginVM.LoginSuccess += this.LoginSuccessful;
            this.LoginRegisterVM = loginVM;
            this.CurrentViewModel = this.LoginRegisterVM;
            var recipeVM = new RecipesViewModel();
            recipeVM.RecipeNavigate += this.RecipeNavigateSuccessful;
            this.RecipeVM = recipeVM;
            this.ViewModels.Add(recipeVM);
            this.ViewModels.Add(new NewRecipeViewModel());
        }

        public void LoginSuccessful(object sender, LoginSuccessArgs e)
        {
            this.Username = e.Username;
            this.LoggedInUser = true;
            this.HandleChangeViewModelCommand(this.ViewModels[0]);
        }

        public void RecipeNavigateSuccessful(object sender, RecipeNavigateArgs e)
        {
            this.HandleChangeViewModelCommand((e.VM as RecipeViewModel));
        }
    }
}