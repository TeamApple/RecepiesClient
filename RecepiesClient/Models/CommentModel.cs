using System;
using System.Linq;

namespace RecepiesClient.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int RecepieId { get; set; }

        public virtual Recepy Recipe { get; set; }
    }
}