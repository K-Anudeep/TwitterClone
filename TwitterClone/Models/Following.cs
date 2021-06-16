using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace TwitterClone.Models
{
    [Table("Following")]
    [Keyless]
    public class Following
    {
        [ForeignKey("UserID")]
        public string User_Id { get; set; }

        [ForeignKey("UserID")]
        public string Following_Id { get; set; }
    }
}
