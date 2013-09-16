using System;
using System.Collections.Generic;
using System.Linq;

namespace RecepiesClient.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Products { get; set; }

        public string CookingSteps { get; set; }

        public string ImagePath { get; set; }

        internal static IEnumerable<string> ParseProducts(string productsAsString)
        {
            var products = productsAsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


            // Tova moje da ne raboti!!!!
            return products;
        }
    }
}