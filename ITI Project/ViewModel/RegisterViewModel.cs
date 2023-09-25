using ITI_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace ITI_Project.ViewModel
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
