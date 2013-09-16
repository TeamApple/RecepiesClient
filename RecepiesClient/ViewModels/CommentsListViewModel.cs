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

        private ObservableCollection<CommentViewModel> commentsList;

        public ObservableCollection<CommentViewModel> Comments { get; set; }

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

        public IEnumerable<CommentViewModel> CommentsList
        {
            get
            {
                if (this.commentsList == null)
                {
                    //TODO: Change recipreId
                    var recipeId = 5;
                    this.CommentsList = DataPersister.GetComments(recipeId);
                }
                return this.commentsList;
            }
            set
            {
                if (this.commentsList == null)
                {
                    this.commentsList = new ObservableCollection<CommentViewModel>();
                }
                this.commentsList.Clear();
                foreach (var item in value)
                {
                    this.commentsList.Add(item);
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
            this.Comments.Clear();
            this.CommentsList = DataPersister.GetComments(recipeId);
        }

        public CommentsListViewModel()
        {
            this.Comments = new ObservableCollection<CommentViewModel>();
            this.Comments.Add(new CommentViewModel()
                {
                    Text = "sample"
                });
        }
    }
}
