using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Request.Comment
{
    public class CommentData
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int ToyId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Author { get; set; }
    }
}
