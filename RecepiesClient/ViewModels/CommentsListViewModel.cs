using RecepiesClient.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecepiesClient.ViewModels
{
    public class CommentsListViewModel:ViewModelBase
    {
        private ICommand addCommentCommand;

        private string text;

        private ObservableCollection<CommentViewModel> comments;

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

        public IEnumerable<CommentViewModel> Comments
        {
            get
            {
                if (this.comments == null)
                {
                    //TODO: Change recipreId
                    var recipeId = 5;
                    this.Comments = DataPersister.GetComments(recipeId);
                }
                return this.comments;
            }
            set
            {
                if (this.comments == null)
                {
                    this.comments = new ObservableCollection<CommentViewModel>();
                }
                this.comments.Clear();
                foreach (var item in value)
                {
                    this.comments.Add(item);
                }
            }
        }

        public ICommand AddComment
        {
            get
            {
                if (this.addCommentCommand== null)
                {
                    this.addCommentCommand = new RelayCommand(this.HandleAddCommentCommand);
                }
                return this.addCommentCommand;
            }
        }

        private void HandleAddCommentCommand(object parameter)
        {
            //TODO: change with true value
            var recipeId = 5;
            var ownerId = 5;
            DataPersister.AddComment(this.Text, recipeId, ownerId);
            this.Text = "";
            this.Comments = DataPersister.GetComments(recipeId);
        }
    }
}
