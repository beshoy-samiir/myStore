using System.ComponentModel.DataAnnotations;

namespace ITI_Project.ViewModel
{
    public class LoginViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
