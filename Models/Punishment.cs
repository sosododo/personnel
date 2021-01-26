using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("punishments")]
    public partial class Punishment
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("punishment_type")]
        [StringLength(20)]
        public string periodType { get; set; }
        [Column("period_type")]
        [StringLength(20)]
        public string PunishmentType { get; set; }
        [Column("reason")]
        public string Reason { get; set; }
        [Column("period")]
        public int? Period { get; set; }
        [Column("start_date", TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column("end_date", TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [Column("discount")]
        public double? Discount { get; set; }
        [Column("notes")]
        [StringLength(30)]
        public string Notes { get; set; }

        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Punishments))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Punishments))]
        public virtual SelfCard Person { get; set; }
    }
}
