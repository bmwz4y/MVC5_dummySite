using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1test1.Models
{
    [Table("dbo.Sometable")]
    public class Class1
    {
        [Key]
        public int SomeId { get; set; }

        //added on lesson 16 this required
        [Required]
        public string Name { get; set; }

        //added on lesson 16 this required
        [Required]
        public string Something { get; set; }
    }
}