using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("bonuses")]
    public partial class Bonuse
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("salary_bouns")]
        public double SalaryBouns { get; set; }
        [Column("salary")]
        public double Salary { get; set; }
        [Column("bouns")]
        public double Bouns { get; set; }
        [Column("num_days")]
        public int NumDays { get; set; }

        [Column("from_year", TypeName = "date")]
        public DateTime? FromYear { get; set; }

        [Column("to_year", TypeName = "date")]
        public DateTime? ToYear { get; set; }

        [Column("job_title")]
        [StringLength(20)]
        public string JobTitle { get; set; }


        [Column("category")]
        [StringLength(30)]
        public string Category { get; set; }

        [Column("work_place")]
        [StringLength(30)]
        public string Workplace { get; set; }


        [Column("register")]
        public string Register { get; set; }


        [ForeignKey(nameof(DecisionId))]
        [InverseProperty(nameof(Models.Decision.Bonuses))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.Bonuses))]
        public virtual SelfCard Person { get; set; }
    }
}
