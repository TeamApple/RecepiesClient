namespace RecepiesClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RecepiesClient.Helpers;
    using RecepiesClient.Models;
    using RecepiesClient.ViewModels;

    public class DataPersister
    {
        protected static string AccessToken { get; set; }

        private const string BaseServicesUrl = "http://recepiesservices.apphb.com/api/";

        internal static void RegisterUser(string username, string authenticationCode)
        {
            Validator.ValidateUsername(username);
            Validator.ValidateAuthCode(authenticationCode);

            var userModel = new UserModel()
            {
                Username = username,
                AuthCode = authenticationCode
            };
            HttpRequester.Post(BaseServicesUrl + "users/register",
                userModel);
        }

        internal static string LoginUser(string username, string authenticationCode)
        {
            Validator.ValidateUsername(username);
            Validator.ValidateAuthCode(authenticationCode);

            var userModel = new UserModel()
            {
                Username = username,
                AuthCode = authenticationCode
            };
            var loginResponse = HttpRequester.Post<LoginResponseModel>(BaseServicesUrl + "auth/token",
                userModel);
            AccessToken = loginResponse.AccessToken;
            return loginResponse.Username;
        }

        internal static bool LogoutUser()
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;
            var isLogoutSuccessful = HttpRequester.Put(BaseServicesUrl + "users/logout", headers);
            return isLogoutSuccessful;
        }

        internal static void CreateNewRecipe(RecipeViewModel recipe)
        {
            var recipeModel = new RecipeModel()
            {
                Name = recipe.Name,
                Products = string.Join(", ", recipe.Products),
                CookingSteps = recipe.CookingSteps,
                ImagePath = recipe.ImagePath
            };

            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;

            var response =
                HttpRequester.Post<RecipeCreatedModel>(BaseServicesUrl + "recipe/new", recipeModel, headers);
        }

        internal static IEnumerable<RecipeViewModel> GetRecipes()
        {
            var headers = new Dictionary<string, string>();
            headers["X-accessToken"] = AccessToken;

            var recipesModels =
                HttpRequester.Get<IEnumerable<RecipeModel>>(BaseServicesUrl + "recipe/all", headers);
            return recipesModels.AsQueryable().
            Select(model => new RecipeViewModel()
                  {
                      Id = model.Id,
                      Name = model.Name,
                      CookingSteps = model.CookingSteps,
                      //Products = RecipeViewModel.ParseProducts(model.Products),
                      Products = model.Products,
                      ImagePath = model.ImagePath
                  });
        }
    }
}