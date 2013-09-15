using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecepiesClient.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CookingSteps { get; set; }

        public string Products { get; set; }

        public string ImagePath { get; set; }
    }
}
