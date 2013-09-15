using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using RecepiesClient.Data;
using TasksManager.WpfClient.Behavior;

namespace RecepiesClient.ViewModels
{
    public class LoginRegisterViewModel : ViewModelBase
    {
        private ICommand loginCommand;
        private string message;
        private ICommand registerCommand;

        public string Username { get; set; }

        public string Email { get; set; }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                this.OnPropertyChanged("Message");
            }
        }

        public ICommand Login
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(this.HandleLoginCommand);
                }
                return this.loginCommand;
            }
        }

        public ICommand Register
        {
            get
            {
                if (this.registerCommand == null)
                {
                    this.registerCommand = new RelayCommand(this.HandleRegisterCommand);
                }
                return this.registerCommand;
            }
        }

        private void HandleLoginCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            string authenticationCode = GetSha1HashData(password);

            var username = DataPersister.LoginUser(this.Username, authenticationCode);
        }

        private void HandleRegisterCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            string authenticationCode = GetSha1HashData(password);

            DataPersister.RegisterUser(this.Username, this.Email, authenticationCode);
            this.HandleLoginCommand(parameter);
        }

        private string GetSha1HashData(string password)
        {
            byte[] buffer = Encoding.Default.GetBytes(password);
            SHA1CryptoServiceProvider cryptoTransformSha1 = new SHA1CryptoServiceProvider();
            string authenticationCode = BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");

            return authenticationCode;
        }
    }
}
