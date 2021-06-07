using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("SalaryIncrease")]
    public partial class SalaryIncrease
    {
        [Key]
        [Column("decision_id")]
        public long DecisionId { get; set; }
        [Key]
        [Column("person_id")]
        public long PersonId { get; set; }
        [Column("salary_after")]
        public double SalaryAfter { get; set; }
        [Column("salary_before")]
        public double SalaryBefore { get; set; }
        [Column("increase")]
        public double Increase { get; set; }

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
        [InverseProperty(nameof(Models.Decision.SalaryIncrease))]
        public virtual Decision Decision { get; set; }
        [ForeignKey(nameof(PersonId))]
        [InverseProperty(nameof(SelfCard.SalaryIncrease))]
        public virtual SelfCard Person { get; set; }
    }
}
