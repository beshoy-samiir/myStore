using System.ComponentModel;

namespace ITI_Project.Models
{
    public class Role
    {
        public int Id { get; set; }
        [DisplayName("Role")]
        public string Name { get; set; }
    }
}
