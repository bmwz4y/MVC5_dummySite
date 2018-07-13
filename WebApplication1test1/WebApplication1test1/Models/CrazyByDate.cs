using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/*
 * added for lesson 29
 */

namespace WebApplication1test1.Models
{
    //[MetadataType(typeof(CrazyByDateMetaData))]
    public class CrazyByDate
    {
        [Key]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public string Crazies { get; set; }
        //public int Crazies { get; set; }
    }

    //public class CrazyByDateMetaData
    //{
        
    //    public DateTime? Date { get; set; }
    //}
}