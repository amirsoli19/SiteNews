using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CMSNewModels.Models
{
    [Table("T_Comment")]
    public class Comment :BaseEntity
    {
        public int CommentId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string CommentText { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int NewsId { get; set; }

        public virtual News  News { get; set; }


    }
}
