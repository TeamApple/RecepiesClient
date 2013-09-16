using System;
using System.Collections.Generic;
using System.Linq;

namespace RecepiesClient.ViewModels
{
    public class RecipeViewModel : ViewModelBase, IPageViewModel
    {
        private string recipeName;
        private string products;
        private string cookingSteps;
        private string imagePath;

        public string Name
        {
            get { return "Recipe view"; }
        }

        //public int Id { get; set; }

        public string RecipeName
        {
            get
            {
                return this.recipeName;
            }
            set
            {
                this.recipeName = value;
                this.OnPropertyChanged("RecipeName");
            }
        }

        public string Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
                this.OnPropertyChanged("Products");
            }
        }
        //public IEnumerable<string> Products { get; set; }

        public string CookingSteps
        {
            get
            {
                return this.cookingSteps;
            }
            set
            {
                this.cookingSteps = value;
                this.OnPropertyChanged("CookingSteps");
            }
        }

        public string ImagePath 
        {
            get 
            {
                return this.imagePath;
            }
            set 
            {
                this.imagePath = value;
                this.OnPropertyChanged("ImagePath");
            }
        }

        //internal static IEnumerable<string> ParseProducts(string productsAsString)
        //{
        //    var products = productsAsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


        //    // Tova moje da ne raboti!!!!
        //    return products;
        //}
    }
}