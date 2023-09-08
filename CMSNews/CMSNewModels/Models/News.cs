using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CMSNewModels.Models
{
    [Table("T_News")]
   public class News: BaseEntity
    {
        [Key]
        public int NewsId { get; set; }
        
        
        public string NewsTitle { get; set; }
        
        public string Description { get; set; }
        
       
        public string ImageName { get; set; }
        
        public DateTime RegisterDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public int See { get; set; }
        
        public int Like { get; set; }
        
        public int NewsGroupId { get; set; }
        
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual NewGroup NewGroup { get; set; }
        public virtual ICollection<Comment>Comments { get; set; }





    }
}
