using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("secondments")]
    public partial class Secondment
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("secondment_type")]
        [StringLength(20)]
        public string SecondmentType { get; set; }
        [Column("period_num")]
        public int? PeriodNum { get; set; }
        [Column("period_type")]
        [StringLength(10)]
        public string PeriodType { get; set; }
        [Column("secondment_place")]
        [StringLength(50)]
        public string SecondmentPlace { get; set; }
        [Column("secondment_start", TypeName = "date")]
        public DateTime? SecondmentStart { get; set; }
        [Column("secondment_end", TypeName = "date")]
        public DateTime? SecondmentEnd { get; set; }
        [Column("notes")]
        [StringLength(50)]
        public string Notes { get; set; }

        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Secondments))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Secondments))]
        public virtual SelfCard Person { get; set; }
    }
}
