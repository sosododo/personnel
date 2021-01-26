using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("jobs")]
    public partial class Job
    {
        [Key]
        [Column("job_id")]
        public int JobId { get; set; }

        [Required]
        [Column("category")]
        [StringLength(30)]
        public string Category { get; set; }

        [Required]
        [Column("job_title")]
        [StringLength(50)]
        public string JobTitle { get; set; }

    }
}
