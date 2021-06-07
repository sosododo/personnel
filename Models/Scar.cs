using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace personnel.Models
{
    [Table("scars")]
    public partial class Scar
    {
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
       
        [Column("scar_reason")]
        [StringLength(50)]
        public string ScarReason { get; set; }
        [Column("period_num")]
        public int? PeriodNum { get; set; }
        [Column("period_type")]
        [StringLength(10)]
        public string PeriodType { get; set; }
        [Column("scar_start", TypeName = "date")]
        public DateTime? ScarStart { get; set; }
        [Column("scar_end", TypeName = "date")]
        public DateTime? ScarEnd { get; set; }
        [Column("scar_place")]
        [StringLength(50)]
        public string ScarPlace { get; set; }
        [Column("notes")]
        [StringLength(50)]
        public string Notes { get; set; }


        [Column("job_title")]
        [StringLength(20)]
        public string JobTitle { get; set; }


        [Column("category")]
        [StringLength(30)]
        public string Category { get; set; }

     

        [Column("salary")]
        public double? Salary { get; set; }

        [Column("register")]
        public string Register { get; set; }

        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Scars))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Scars))]
        public virtual SelfCard Person { get; set; }
    }
}
