using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RecepiesClient.Behavior;
using RecepiesClient.Data;

namespace RecepiesClient.ViewModels
{
    public class CommentsListViewModel : ViewModelBase
    {
        private ICommand addCommentCommand;

        private string text;
        private string recipeId;
        private string ownerId;

        private ObservableCollection<CommentViewModel> commentsList;

        private CommentViewModel newCommentViewModel;

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

        public int RecipeId { get; set; }

        public int OwnerId { get; set; }

        public IEnumerable<CommentViewModel> CommentsList
        {
            get
            {
                if (this.commentsList == null)
                {
                    //TODO: Change recipreId
                    var recipeId = 5;
                    LoadCommentsAsync(recipeId);
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
  
        private async void LoadCommentsAsync(int recipeId)
        {
            this.CommentsList = await DataPersister.GetCommentsAsync(recipeId);
        }

        public CommentViewModel NewComment
        {
            get
            {
                return this.newCommentViewModel;
            }
            set
            {
                this.newCommentViewModel = value;
                this.OnPropertyChanged("NewComment");
            }
        }

        public ICommand AddComment
        {
            get
            {
                if (this.addCommentCommand == null)
                {
                    this.addCommentCommand = new RelayCommand(this.HandleAddCommentCommand);
                }
                return this.addCommentCommand;
            }
        }

        private async void HandleAddCommentCommand(object parameter)
        {
            this.OwnerId = DataPersister.GetUserId();

            if (!string.IsNullOrEmpty(this.Text))
            {
                DataPersister.AddComment(this.Text, this.RecipeId, this.OwnerId);
                this.Text = "";
                this.CommentsList = await DataPersister.GetCommentsAsync(this.RecipeId);
            }
        }

        public CommentsListViewModel()
        {
            this.newCommentViewModel = new CommentViewModel();
        }
    }
}