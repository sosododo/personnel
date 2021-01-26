using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personnel.Models
{
    [Table("users")]
    public partial class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("user_name")]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [Column("password")]
        [StringLength(10)]
        public string Password { get; set; }
        //[Column("name")]
        //[StringLength(50)]
        //public string Name { get; set; }
        [Column("rule")]
        [StringLength(50)]
        public string Rule { get; set; }
    }
}
