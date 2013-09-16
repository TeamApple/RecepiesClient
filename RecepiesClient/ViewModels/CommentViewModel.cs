using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesClient.ViewModels
{
    public class CommentViewModel:ViewModelBase
    {
        private string text;
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }

        public int Id { get; set; }

        public int OwnerId { get; set; }
    }
}
