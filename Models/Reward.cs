using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("rewards")]
    public partial class Reward
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Required]
        [Column("Reward_type")]
        [StringLength(30)]
        public string RewardType { get; set; }
        [Column("reason")]
        [StringLength(30)]
        public string Reason { get; set; }
        [Column("reward_date", TypeName = "date")]
        public DateTime? RewardDate { get; set; }
        [Column("amount")]
        [StringLength(30)]
        public string Amount { get; set; }
        [Column("notes")]
        [StringLength(30)]
        public string Notes { get; set; }
    }
}
