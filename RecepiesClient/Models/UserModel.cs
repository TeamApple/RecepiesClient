namespace RecepiesClient.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserModel
    {

        public string Username { get; set; }

        public string AuthCode { get; set; }
    }
}
