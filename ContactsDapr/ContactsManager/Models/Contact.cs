using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Models
{

    public class Contact
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public required string Name { get; set; }

        [CheckIsraelNumber(ErrorMessage = "Phone number you supplied is not a valid israel phone number.")]
        public required string Phone { get; set; }
    }
}
