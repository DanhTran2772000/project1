using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppUniversity.Data
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }

        public int DepartmentId { get; set; }
        public virtual ICollection<Department>? Departments { get; set; }
    }

    
}
