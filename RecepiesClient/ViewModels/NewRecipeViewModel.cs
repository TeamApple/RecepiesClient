namespace RecepiesClient.ViewModels
{
    using System;
    using System.Windows.Input;
    using RecepiesClient.Data;

    public class NewRecipeViewModel : ViewModelBase, IPageViewModel
    {
        private ICommand addRecipeCommand;

        public NewRecipeViewModel()
        {
            this.NewRecipe = new RecipeViewModel();
        }

        public RecipeViewModel NewRecipe { get; set; }

        public string Name
        {
            get
            {
                return "Add New Recipe View";
            }
        }

        public ICommand AddRecipe 
        {
            get
            {
                if (this.addRecipeCommand == null)
                {
                    this.addRecipeCommand = new RelayCommand(this.HandleAddNewRecipeCommand);
                }

                return this.addRecipeCommand;
            }
        }

        private void HandleAddNewRecipeCommand(object parameter)
        {
            DataPersister.CreateNewRecipe(this.NewRecipe);
        }
    }
}
