using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("self_cards")]
    public partial class SelfCard
    {
        public SelfCard()
        {
            SalaryIncrease = new HashSet<SalaryIncrease>();
            Bonuses = new HashSet<Bonuse>();
            Delegatings = new HashSet<Delegating>();
            FunctionalChanges = new HashSet<FunctionalChange>();
            Punishments = new HashSet<Punishment>();
            Rests = new HashSet<Rest>();
            Scars = new HashSet<Scar>();
            Secondments = new HashSet<Secondment>();
        }

        [Key]
        [Column("person_id")]

        public long PersonId { get; set; }


        [Column("employee_id")]
        public long? EmployeeId { get; set; }
        [Column("insurance_id")]
        [StringLength(50)]
        public string InsuranceId { get; set; }
        [Column("nation_id")]
        [StringLength(50)]
        public string NationId { get; set; }
        [Column("file_id")]
        [StringLength(50)]
        public string FileId { get; set; }
        [Required]
        [Column("file_class")]
        [StringLength(50)]
        public string FileClass { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("father_name")]
        [StringLength(30)]
        public string FatherName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Column("mother_name")]
        [StringLength(50)]
        public string MotherName { get; set; }
        [Column("birth_place")]
        [StringLength(20)]
        public string BirthPlace { get; set; }
        [Column("birthday", TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [Column("registered_id")]
        [StringLength(20)]
        public string RegisteredId { get; set; }
        [Column("nationality")]
        [StringLength(20)]
        public string Nationality { get; set; }
        [Column("sex")]
        [StringLength(10)]
        public string Sex { get; set; }
        [Column("social_situation")]
        [StringLength(10)]
        public string SocialSituation { get; set; }
        [Column("address")]
        [StringLength(30)]
        public string Address { get; set; }
        [Column("phone")]
        [StringLength(20)]
        public string Phone { get; set; }
        [Column("mobile")]
        [StringLength(50)]
        public string Mobile { get; set; }
        [Column("military")]
        [StringLength(20)]
        public string Military { get; set; }
        [Column("recruitment")]
        [StringLength(20)]
        public string Recruitment { get; set; }
        [Column("religion")]
        [StringLength(10)]
        public string Religion { get; set; }
        [Column("photo")]
        [StringLength(50)]
        public string Photo { get; set; }
        [Column("job_title")]
        [StringLength(20)]
        public string JobTitle { get; set; }
        [Column("employer")]
        [StringLength(30)]
        public string Employer { get; set; }
        [Column("workplace")]
        [StringLength(30)]
        public string Workplace { get; set; }
        [Column("section")]
        [StringLength(30)]
        public string Section { get; set; }
        [Column("division")]
        [StringLength(30)]
        public string Division { get; set; }
        [Column("mission")]
        [StringLength(30)]
        public string Mission { get; set; }
        [Column("category")]
        [StringLength(30)]
        public string Category { get; set; }
        [Column("status")]
        [StringLength(30)]
        public string Status { get; set; }
        [Column("salary")]
        public double? Salary { get; set; }
        [Required]
        [Column("beginning_date", TypeName = "date")]
        public DateTime? BeginningDate { get; set; }
        [Column("certificate")]
        [StringLength(30)]
        public string Certificate { get; set; }
        [Column("specialization")]
        [StringLength(30)]
        public string Specialization { get; set; }
        [Column("nomination_type")]
        [StringLength(20)]
        public string NominationType { get; set; }
        [Column("insurance_card")]
        [StringLength(20)]
        public string InsuranceCard { get; set; }
        [Column("notes")]
        [StringLength(30)]
        public string Notes { get; set; }
        [Column("register")]
        public string Register { get; set; }
        [Column("languages")]
        [StringLength(50)]
        public string Languages { get; set; }
        [Column("training_courses")]
        [StringLength(50)]
        public string TrainingCourses { get; set; }
        [Column("work_contract")]
        [StringLength(30)]
        public string WorkContracts { get; set; }

        [Column("isTeacher")]
       
        public int IsTeacher { get; set; }

        [Column("maxsalary")]
        public double? maxsalary { get; set; }


        [InverseProperty("Person")]
        public virtual ICollection<Bonuse> Bonuses { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<SalaryIncrease> SalaryIncrease { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Delegating> Delegatings { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<FunctionalChange> FunctionalChanges { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Punishment> Punishments { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Rest> Rests { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Scar> Scars { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<Secondment> Secondments { get; set; }

        public string Username
        {
            get { return FirstName + ' ' + FatherName + ' '  + LastName ; }
        }

    }
    
}
