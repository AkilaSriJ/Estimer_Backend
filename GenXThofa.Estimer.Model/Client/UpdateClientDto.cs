using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Client
{
    public class UpdateClientDto
    {
        [Required(ErrorMessage = "Client Name is Required")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+(?: [a-zA-Z]+)*$", ErrorMessage = "Client Name should Contain only letters")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Address must be 5-250 characters")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Address cannot be empty or spaces only")]
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
