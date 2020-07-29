using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("rests")]
    public partial class Rest
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("rest_type")]
        [StringLength(20)]
        public string RestType { get; set; }
        [Column("rest_period")]
        [StringLength(20)]
        public string RestPeriod { get; set; }
        [Column("rest_start", TypeName = "date")]
        public DateTime? RestStart { get; set; }
        [Column("rest_end", TypeName = "date")]
        public DateTime? RestEnd { get; set; }
        [Column("notes")]
        [StringLength(30)]
        public string Notes { get; set; }

        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Rests))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Rests))]
        public virtual SelfCard Person { get; set; }
    }
}
