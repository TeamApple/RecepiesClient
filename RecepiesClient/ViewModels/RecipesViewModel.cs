using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using RecepiesClient.Data;

namespace RecepiesClient.ViewModels
{
    public class RecipesViewModel : ViewModelBase, IPageViewModel
    {
        private string title;
        private ObservableCollection<RecipeViewModel> recipes;

        public ICommand NavigateToRecipeCommand { get; set; }

        public RecipeViewModel SelectedRecipe { get; set; }

        private void HandleNavigateToRecipeCommand(object obj)
        {
            if (SelectedRecipe == null)
            {
                return;
            }

            MessageBox.Show(SelectedRecipe.Name);
        }

        public RecipesViewModel()
        {
            this.NavigateToRecipeCommand = new RelayCommand(this.HandleNavigateToRecipeCommand);
        }

        public string Name
        {
            get
            {
                return "Recipe list view";
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        public IEnumerable<RecipeViewModel> Recipes
        {
            get
            {
                if (this.recipes == null)
                {
                    this.Recipes = DataPersister.GetRecipes();
                }
                return this.recipes;
            }
            set
            {
                if (this.recipes == null)
                {
                    this.recipes = new ObservableCollection<RecipeViewModel>();
                }
                this.recipes.Clear();
                foreach (var item in value)
                {
                    this.recipes.Add(item);
                }
            }
        }
    }
}