using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppUniversity.Data
{
    [Table("Departments")]
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
