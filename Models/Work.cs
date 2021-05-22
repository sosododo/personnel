using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("works")]
    public partial class Work
    {
        [Key]
        [Column("place_id")]
       
        public int PlaceId { get; set; }

        [Required]
        [Column("work_place")]
        [StringLength(100)]
        public string WorkPlace { get; set; }
        
    }
}
