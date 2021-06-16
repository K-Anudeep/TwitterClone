using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [MaxLength(25)]
        [Required]
        public string UserID { get; set; }
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [MaxLength(30)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Joined { get; set; }
        [Required]
        public byte Active { get; set; }
    }
}
