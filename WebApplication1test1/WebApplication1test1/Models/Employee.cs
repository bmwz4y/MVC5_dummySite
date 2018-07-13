using System.ComponentModel;
using WebApplication1test1.Common;

namespace WebApplication1test1.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        //added on lesson 89 removed on 91
        //[Remote("IsEmailAvailable","Employees",ErrorMessage = "Email already registered!")]
        //added on lesson 91
        [RemoteClientServer("IsEmailAvailable", "Employees", ErrorMessage = "Email already registered!!!")]
        [StringLength(50)]
        public string Email { get; set; }
    }

    public partial class Employee
    {
        //added on lesson 85
        [NotMapped, Compare("Email"), DisplayName("Type Email Again")]
        public string EmailConfirm
        {
            get;
            set;
        }
    }
}