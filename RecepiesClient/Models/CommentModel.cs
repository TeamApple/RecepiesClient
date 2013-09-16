using System;
using System.Linq;
using System.Runtime.Serialization;

namespace RecepiesClient.Models
{
    [DataContract]
    public class CommentModel
    {
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "ownerId")]
        public int OwnerId { get; set; }

        [DataMember(Name = "recepieId")]
        public int RecepieId { get; set; }

    }
}