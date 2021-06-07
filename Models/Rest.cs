using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        public int RestPeriod { get; set; }
        [Column("rest_start", TypeName = "date")]
        public DateTime? RestStart { get; set; }
        [Column("rest_end", TypeName = "date")]
        public DateTime? RestEnd { get; set; }
        [Column("notes")]
        [StringLength(30)]
        public string Notes { get; set; }
        [Column("attachment")]
        [StringLength(100)]
        public string Attachment { get; set; }
        [Column("period")]
        [StringLength(30)]
        public string Period { get; set; }


        [Column("job_title")]
        [StringLength(20)]
        public string JobTitle { get; set; }


        [Column("category")]
        [StringLength(30)]
        public string Category { get; set; }

        [Column("work_place")]
        [StringLength(30)]
        public string Workplace { get; set; }

        [Column("salary")]
        public double? Salary { get; set; }

        [Column("register")]
        public string Register { get; set; }


        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Rests))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Rests))]
        public virtual SelfCard Person { get; set; }




        public string getdata(long desId)
        {
            PersonelDBContext db = new PersonelDBContext();
            Decision des;
            des = (Decision)db.Decisions.Where(x => x.DecisionId == desId);
            string result = des.DecisionNumber + ' ' + des.DecisionType + ' ' + des.DecisionYear;
            return result;
        }
    }
}
