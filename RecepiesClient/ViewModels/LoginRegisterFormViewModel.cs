namespace RecepiesClient.ViewModels
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;
    using RecepiesClient.Behavior;
    using RecepiesClient.Data;
    using RecepiesClient.Helpers;

    public class LoginRegisterFormViewModel : ViewModelBase, IPageViewModel
    {
        private string message;
        private ICommand loginCommand;
        private ICommand registerCommand;

        public string Name
        {
            get
            {
                return "Login Form";
            }
        }

        public string Username { get; set; }

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

        public event EventHandler<LoginSuccessArgs> LoginSuccess;

        public LoginRegisterFormViewModel()
        {
        }

        private async void HandleRegisterCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            var authenticationCode = this.GetSha1HashData(password);
            try
            {
                 await Task.Factory
                .StartNew(() => DataPersister.RegisterUserAsync(this.Username, authenticationCode))
                .ContinueWith((continuation) => this.HandleLoginCommand(parameter));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void HandleLoginCommand(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            var authenticationCode = this.GetSha1HashData(password);

            string username = null;

            try
            {
                username = await DataPersister.LoginUserAsync(this.Username, authenticationCode);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid username or password");
            }

            if (!string.IsNullOrEmpty(username))
            {
                this.RaiseLoginSuccess(username);
            }
        }

        private string GetSha1HashData(string data)
        {
            byte[] buffer = Encoding.Default.GetBytes(data);
            SHA1CryptoServiceProvider cryptoTransformSha1 = new SHA1CryptoServiceProvider();
            string hashedString = BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");

            return hashedString;
        }

        protected void RaiseLoginSuccess(string username)
        {
            if (this.LoginSuccess != null)
            {
                this.LoginSuccess(this, new LoginSuccessArgs(username));
            }
        }
    }
}