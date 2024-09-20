using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetStore.API.Models.Request.Order
{
    public class OrderRequest
    {
        [Required]
        public List<OrderItemRequest> OrderItems { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        public string TokenId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Maximum length is 30")]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Maximum length is 30")]
        public string CustomerSurname { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        public string Country { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        public string City { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Maximum length is 80")]
        public string StreetAddress { get; set; }
    }
}