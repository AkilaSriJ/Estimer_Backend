using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Client
{
    public class CreateClientDto
    {
        [Required(ErrorMessage = "Client name is required")]
        [MaxLength(200, ErrorMessage = "Client name cannot exceed 200 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Contact Person name is required")]
        [MaxLength(150, ErrorMessage = "contact person name cannot exceed 150 characters")]
        public string CompanyContactPerson { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email Format ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(20, ErrorMessage ="Phone number cannot exceed 20 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required")]
        [MaxLength(250, ErrorMessage = "Address Line 1 cannot exceed 250 characters")]
        public string AddressLine1 { get; set; }

        [MaxLength(250, ErrorMessage = "Address Line 2 cannot exceed 250 characters")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State/Province is required")]
        [MaxLength(100, ErrorMessage = "State/Province cannot exceed 100 characters")]
        public string StateProvince { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
        public string Country { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
