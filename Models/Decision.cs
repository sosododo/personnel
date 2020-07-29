using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("decisions")]
    public partial class Decision
    {
        public Decision()
        {
            Bonuses = new HashSet<Bonuse>();
            Delegatings = new HashSet<Delegating>();
            FunctionalChanges = new HashSet<FunctionalChange>();
            Punishments = new HashSet<Punishment>();
            Rests = new HashSet<Rest>();
            Secondments = new HashSet<Secondment>();
        }

        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Column("decision_number")]
        public int DecisionNumber { get; set; }
        [Required]
        [Column("decision_type")]
        [StringLength(30)]
        public string DecisionType { get; set; }
        [Column("decision_year")]
        public int DecisionYear { get; set; }
        [Column("decision_start", TypeName = "date")]
        public DateTime? DecisionStart { get; set; }
        [Column("decision_end", TypeName = "date")]
        public DateTime? DecisionEnd { get; set; }
        [Column("decision_content")]
        [StringLength(30)]
        public string DecisionContent { get; set; }
        [Column("decision_source")]
        [StringLength(30)]
        public string DecisionSource { get; set; }
        [Column("effect_type")]
        [StringLength(10)]
        public string EffectType { get; set; }
        [Column("attachment")]
        public string Attachment { get; set; }
        [Column("result")]
        public bool Result { get; set; }

        [InverseProperty("Decision")]
        public virtual ICollection<Bonuse> Bonuses { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Delegating> Delegatings { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<FunctionalChange> FunctionalChanges { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Punishment> Punishments { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Rest> Rests { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Secondment> Secondments { get; set; }
    }
}
