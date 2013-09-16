namespace RecepiesClient.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract]
    public class RecipeCreatedModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}