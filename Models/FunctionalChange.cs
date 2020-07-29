using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("functional_changes")]
    public partial class FunctionalChange
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("work_place")]
        [StringLength(30)]
        public string WorkPlace { get; set; }
        [Column("job_title")]
        [StringLength(20)]
        public string JobTitle { get; set; }
        [Column("mission")]
        [StringLength(30)]
        public string Mission { get; set; }
        [Column("category")]
        [StringLength(30)]
        public string Category { get; set; }
        [Column("status")]
        [StringLength(30)]
        public string Status { get; set; }
        [Column("salary")]
        public double? Salary { get; set; }
        [Column("change_reasone")]
        [StringLength(50)]
        public string ChangeReasone { get; set; }
        [Column("register")]
        [StringLength(30)]
        public string Register { get; set; }
        [Column("change_date", TypeName = "date")]
        public DateTime ChangeDate { get; set; }
        [Column("period_num")]
        public int? PeriodNum { get; set; }

        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.FunctionalChanges))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.FunctionalChanges))]
        public virtual SelfCard Person { get; set; }
    }
}
