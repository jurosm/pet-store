using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Comment
{
    public class CommentsUnit
    {
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public string Author { get; set; }

    }
}
