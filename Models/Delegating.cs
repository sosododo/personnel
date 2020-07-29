using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("delegatings")]
    public partial class Delegating
    {
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Column("delegating_type")]
        [StringLength(20)]
        public string DelegatingType { get; set; }
        [Column("delegating_reason")]
        [StringLength(50)]
        public string DelegatingReason { get; set; }
        [Column("period_num")]
        public int? PeriodNum { get; set; }
        [Column("period_type")]
        [StringLength(10)]
        public string PeriodType { get; set; }
        [Column("delegating_start", TypeName = "date")]
        public DateTime? DelegatingStart { get; set; }
        [Column("delegating_end", TypeName = "date")]
        public DateTime? DelegatingEnd { get; set; }
        [Column("delegating_country")]
        [StringLength(50)]
        public string DelegatingCountry { get; set; }
        [Column("notes")]
        [StringLength(50)]
        public string Notes { get; set; }

        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Delegatings))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Delegatings))]
        public virtual SelfCard Person { get; set; }
    }
}
