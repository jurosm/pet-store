using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Request.Comment
{
    public class CommentData
    {
        public string Text { get; set; }
        public int ToyId { get; set; }
        public string Author { get; set; }
    }
}
