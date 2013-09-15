namespace RecepiesClient.Helpers
{
    using System;
    using System.Linq;

    public class LoginSuccessArgs : EventArgs
    {
        public string Username { get; set; }

        public LoginSuccessArgs(string username)
            : base()
        {
            this.Username = username;
        }
    }
}
