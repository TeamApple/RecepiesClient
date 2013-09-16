namespace RecepiesClient.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using RecepiesClient.Behavior;
    using RecepiesClient.Data;
    using System.Windows.Forms;

    public class NewRecipeViewModel : ViewModelBase, IPageViewModel
    {
        private ICommand addRecipeCommand;
        private ICommand addImageCommand;

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

        public ICommand AddImage
        {
            get
            {
                if (this.addImageCommand == null)
                {
                    this.addImageCommand = new RelayCommand(
                        (e) =>
                        {
                            OpenFileDialog fileDialog = new OpenFileDialog();
                            fileDialog.Multiselect = true;
                            fileDialog.ShowDialog();
                            if (!fileDialog.FileNames.Any())
                            {
                                return;
                            }

                            this.NewRecipe.ImagePath = fileDialog.FileName;
                        });
                }

                return addImageCommand;
            }
        }


        private void HandleAddNewRecipeCommand(object parameter)
        {
            DataPersister.CreateNewRecipe(this.NewRecipe);
        }
    }
}
