using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CMSNewModels.Models
{
    [Table("T_Users")]
    public class User:BaseEntity
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection <News>Newses { get; set; }

    }
}
