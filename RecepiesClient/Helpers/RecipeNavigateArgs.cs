using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesClient.Helpers
{
    public class RecipeNavigateArgs : EventArgs
    {
        public object VM { get; set; }

        public RecipeNavigateArgs(object vm)
            : base()
        {
            this.VM = vm;
        }
    }
}
