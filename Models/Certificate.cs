using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("certificate")]
    public partial class Certificate
    {
        [Key]
        [Column("cert_id")]
        public int CertId { get; set; }

        

        [Required]
        [Column("cert_name")]
        [StringLength(50)]
        public string CertName { get; set; }

    }
}
