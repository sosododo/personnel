using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace personnel.Models
{
    [Table("decisions")]
    public partial class Decision
    {
        public Decision()
        {
            SalaryIncrease = new HashSet<SalaryIncrease>();
              Bonuses = new HashSet<Bonuse>();
            Delegatings = new HashSet<Delegating>();
            FunctionalChanges = new HashSet<FunctionalChange>();
            Punishments = new HashSet<Punishment>();
            Rests = new HashSet<Rest>();
            Secondments = new HashSet<Secondment>();
            Scars = new HashSet<Scar>();
            Full = DecisionNumber + " " + DecisionType + " " + DecisionYear;
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

        [Column("collaps_date", TypeName = "date")]
        public DateTime? CollapsDate { get; set; }
       
        [Column("decision_content")]
        [StringLength(30)]
        public string DecisionContent { get; set; }
        [Column("decision_source")]
        [StringLength(30)]
        public string DecisionSource { get; set; }

        [Column("decision_status")]
        [StringLength(30)]
        public string DecisionStatus { get; set; }

        
        [Column("effect_type")]
        [StringLength(10)]
        public string EffectType { get; set; }
        [Column("attachment")]
        public string Attachment { get; set; }
        [Column("result")]
        [StringLength(50)]
        public string Result { get; set; }

        [Column("full")]
      
        public string Full { get; set; }


        [Column("isexcute")]

        public bool IsExcute { get; set; }


        //[DefaultValue("N'false'")]

        [InverseProperty("Decision")]
        public virtual ICollection<Bonuse> Bonuses { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<SalaryIncrease> SalaryIncrease { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Delegating> Delegatings { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Scar> Scars { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<FunctionalChange> FunctionalChanges { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Punishment> Punishments { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Rest> Rests { get; set; }
        [InverseProperty("Decision")]
        public virtual ICollection<Secondment> Secondments { get; set; }

        public string getdata()
        {
            
            string result = DecisionNumber + " " +DecisionType + " " + DecisionYear;
            return result;
        }

        
    }
}
