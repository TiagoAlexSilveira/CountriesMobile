using System.ComponentModel.DataAnnotations;

namespace OnSale.Common.Requests
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
