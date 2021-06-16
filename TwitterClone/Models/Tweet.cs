using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Models
{
    [Table("Tweet")]
    public class Tweet
    {
        [Key]
        [MaxLength(10)]
        [Required]
        public int TweetId { get; set; }
        [ForeignKey("UserID")]
        public string User_ID { get; set; }
        [MaxLength(140)]
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
