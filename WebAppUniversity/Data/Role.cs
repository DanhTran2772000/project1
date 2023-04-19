using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAppUniversity.Data
{
    [Table("Roles")]
    public class Role
    {
        public string ID { get; set; }
        public string UserRole { get; set; }

    }
}
