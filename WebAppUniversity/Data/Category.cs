using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppUniversity.Data
{
    [Table("Categorys")]
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
