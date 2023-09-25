using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ITI_Project.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

    }
}
