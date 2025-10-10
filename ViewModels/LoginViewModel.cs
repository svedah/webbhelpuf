using System.ComponentModel.DataAnnotations;

namespace webbhelpuf.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public required string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}