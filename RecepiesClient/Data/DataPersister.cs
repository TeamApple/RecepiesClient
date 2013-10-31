namespace RecepiesClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RecepiesClient.Helpers;
    using RecepiesClient.Models;
    using RecepiesClient.ViewModels;

    public class DataPersister
    {
        protected static int UserId { get; set; }

        protected static string AccessToken { get; set; }

        private const string BaseServicesUrl = "http://recepiesservices.apphb.com/api/";

        internal async static void RegisterUserAsync(string username, string authenticationCode)
        {
            Validator.ValidateUsername(username);
            Validator.ValidateAuthCode(authenticationCode);

            var userModel = new UserModel()
            {
                Username = username,
                AuthCode = authenticationCode
            };
            
            await HttpRequester.PostAsync<UserModel>(BaseServicesUrl + "users/register",
                userModel);
        }

        internal async static Task<string> LoginUserAsync(string username, string authenticationCode)
        {
            Validator.ValidateUsername(username);
            Validator.ValidateAuthCode(authenticationCode);

            var userModel = new UserModel()
            {
                Username = username,
                AuthCode = authenticationCode
            };

            var loginResponse = await HttpRequester.PostAsync<LoginResponseModel>(BaseServicesUrl + "auth/token",
                userModel);

            UserId = loginResponse.Id;
            AccessToken = loginResponse.AccessToken;
            return loginResponse.Username;
        }

        internal async static Task<bool> LogoutUserAsync()
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;
            var isLogoutSuccessful = await HttpRequester.Put(BaseServicesUrl + "users/logout", headers);
            
            return isLogoutSuccessful;
        }

        internal static void CreateNewRecipe(RecipeViewModel recipe)
        {
            var recipeModel = new RecipeModel()
            {
                Name = recipe.RecipeName,
                Products = string.Join(", ", recipe.Products),
                CookingSteps = recipe.CookingSteps,
                ImagePath = recipe.ImagePath
            };

            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;

            var response =
                HttpRequester.PostAsync<RecipeCreatedModel>(BaseServicesUrl + "recipe/new", recipeModel, headers);
        }

        internal async static Task<IEnumerable<RecipeModel>> GetRecipesAsync()
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;

            var recipesModels = await
                HttpRequester.GetAsync<IEnumerable<RecipeModel>>(BaseServicesUrl + "recipe/all", headers);

            return recipesModels;
        }

        internal async static Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int recipeId)
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;

            var commentsModels = await
                HttpRequester.GetAsync<IEnumerable<CommentModel>>(BaseServicesUrl + "comment/" + recipeId, headers);
            return commentsModels.AsQueryable().
            Select(model => new CommentViewModel()
                  {
                      Id = model.Id,
                      Text = model.Text,
                      OwnerId = model.OwnerId
                  });
        }

        internal async static void AddComment(string comment, int recipeId, int ownerId)
        {
            var commentModel = new CommentModel()
            {
                Text = comment,
                RecepieId = recipeId,
                OwnerId = ownerId
            };

            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;

            var response = await
                HttpRequester.PostAsync<CreatedComment>(BaseServicesUrl + "comment/new", commentModel, headers);
        }

        internal static int GetUserId()
        {
            return UserId;
        }
    }
}