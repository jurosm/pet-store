using System;
using System.Collections.Generic;

namespace PetStore.API.Db
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int? ToyId { get; set; }
        public DateTime DatePosted { get; set; }

        public virtual Toy Toy { get; set; }
    }
}
