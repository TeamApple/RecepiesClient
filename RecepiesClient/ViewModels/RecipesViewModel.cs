using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using RecepiesClient.Behavior;
using RecepiesClient.Data;
using RecepiesClient.Helpers;
using RecepiesClient.Models;

namespace RecepiesClient.ViewModels
{
    public class RecipesViewModel : ViewModelBase, IPageViewModel
    {
        private string title;
        private ICommand navigateToRecipe;
        private ObservableCollection<RecipeModel> recipes;

        public ICommand NavigateToRecipeCommand
        {
            get
            {
                if (this.navigateToRecipe == null)
                {
                    this.navigateToRecipe = new RelayCommand(this.HandleNavigateToRecipeCommand);
                }

                return this.navigateToRecipe;
            }
        }

        public event EventHandler<RecipeNavigateArgs> RecipeNavigate;

        public RecipeModel SelectedRecipe { get; set; }

        private async void HandleNavigateToRecipeCommand(object obj)
        {
            if (SelectedRecipe == null)
            {
                return;
            }

            var newVM = new RecipeViewModel() { 
                RecipeName = this.SelectedRecipe.Name,
                Products = this.SelectedRecipe.Products,
                CookingSteps = this.SelectedRecipe.CookingSteps,
                ImagePath = this.SelectedRecipe.ImagePath,
                Comments = new CommentsListViewModel(){
                    CommentsList = await DataPersister.GetCommentsAsync(this.SelectedRecipe.Id),
                    RecipeId = this.SelectedRecipe.Id
                }
            };

            this.RaiseRecipeNavigate(newVM);
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

        public IEnumerable<RecipeModel> Recipes
        {
            get
            {
                if (this.recipes == null)
                {
                    LoadRecipesAsync();
                }

                return this.recipes;
            }
            set
            {
                if (this.recipes == null)
                {
                    this.recipes = new ObservableCollection<RecipeModel>();
                }
                this.recipes.Clear();
                foreach (var item in value)
                {
                    this.recipes.Add(item);
                }
                this.OnPropertyChanged("Recipes");
            }
        }
  
        private async void LoadRecipesAsync()
        {
            this.Recipes = await DataPersister.GetRecipesAsync();
        }

        protected void RaiseRecipeNavigate(RecipeViewModel vm)
        {
            if (this.RecipeNavigate != null)
            {
                this.RecipeNavigate(this, new RecipeNavigateArgs(vm));
            }
        }
    }
}