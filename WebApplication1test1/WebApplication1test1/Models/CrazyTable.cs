namespace WebApplication1test1.Models
{
    /*
     * this was created for lesson 26 but changed after last lesson from designer to normal codeFirst EF along with it's context Model
     */

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("crazyTable")] /*was with [MetadataType(typeof(CrazyMetaData))]*/
    public class CrazyTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //added at lesson 83
        [RegularExpression(@"[a-zA-Z]+([a-zA-Z0-9]*)", ErrorMessage = "Must be letters and numbers only!")]
        //added at lesson 80
        [StringLength(32, MinimumLength = 3)] //changed from length(50)
        public string Crazy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "date //changed table header")]
        [Column(TypeName = "date")]
        public DateTime? CrazyDate { get; set; }
    }

    //public class CrazyMetaData
    //{
    //    //then removed at lesson 28
    //    //added on lesson 27
    //    /*[Required]*/

    //    //added at lesson 83
    //    [RegularExpression(@"[a-zA-Z]+([a-zA-Z0-9]*)", ErrorMessage = "Must be letters and numbers only!")]
    //    //added at lesson 80
    //    [StringLength(50, MinimumLength = 3)]
    //    public string Crazy { get; set; }

    //    /* Display was added in lesson 26 but the rest on lesson 27*/

    //    [Required]
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "date //changed table header")]
    //    public DateTime? Crazydate { get; set; }
    //}
}
