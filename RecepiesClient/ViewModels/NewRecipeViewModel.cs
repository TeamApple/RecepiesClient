namespace RecepiesClient.ViewModels
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Xml.Linq;
    using RecepiesClient.Behavior;
    using RecepiesClient.Data;

    public class NewRecipeViewModel : ViewModelBase, IPageViewModel
    {
        private ICommand addRecipeCommand;
        private ICommand addImageCommand;

        public NewRecipeViewModel()
        {
            this.NewRecipe = new RecipeViewModel();
        }

        public RecipeViewModel NewRecipe { get; set; }

        public string Name
        {
            get
            {
                return "Add New Recipe View";
            }
        }

        public ICommand AddRecipe 
        {
            get
            {
                if (this.addRecipeCommand == null)
                {
                    this.addRecipeCommand = new RelayCommand(this.HandleAddNewRecipeCommand);
                }

                return this.addRecipeCommand;
            }
        }

        public ICommand AddImage
        {
            get
            {
                if (this.addImageCommand == null)
                {
                    this.addImageCommand = new RelayCommand(
                        (e) =>
                        {
                            OpenFileDialog fileDialog = new OpenFileDialog();
                            fileDialog.Multiselect = true;
                            fileDialog.ShowDialog();
                            if (!fileDialog.FileNames.Any())
                            {
                                return;
                            }

                            string imagePath = this.UploadImageToImgur(fileDialog.FileName);

                            this.NewRecipe.ImagePath = imagePath;
                        });
                }

                return addImageCommand;
            }
        }


        private void HandleAddNewRecipeCommand(object parameter)
        {
            DataPersister.CreateNewRecipe(this.NewRecipe);
        }

        private string UploadImageToImgur(string imagePath)
        {
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                    { "key", "433a1bf4743dd8d7845629b95b5ca1b4" },
                    { "image", Convert.ToBase64String(File.ReadAllBytes(imagePath)) }
                };

                byte[] response = w.UploadValues("http://imgur.com/api/upload.xml", values);

                return XDocument.Load(new MemoryStream(response)).Root.Element("original_image").Value;
            }
        }
    }
}
