using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class UserInfoViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string UserEmail { get; set; }
    }
}