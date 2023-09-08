using CMSNewModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSNews.Models.ViewModels
{
    public class NewsViewModel
    {
        [Display(Name ="کد خبر")]
        public int NewsId { get; set; }
        [Required]
        [MaxLength(300)]
        [Display(Name = "عنوان خبر")]

        public string NewsTitle { get; set; }
        [Required]
        [Display(Name = "توضیحات خبر")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]

        public string Description { get; set; }
        [Display(Name = "تصویر خبر")]
        [MaxLength(100)]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ خبر")]
        [DisplayFormat(DataFormatString ="{0: dddd, dd MMMM yyyy}")]

        public DateTime RegisterDate { get; set; }
        
        [Display(Name = "فعال/غیرفعال")]

        public bool IsActive { get; set; }
        
        [Display(Name = "بازدید")]

        public int See { get; set; }
        
        [Display(Name = "لایک")]

        public int Like { get; set; }
        
        [Display(Name =" گروه خبری")]
        public int NewsGroupId { get; set; }
        
        [Display(Name = " کاربر ثبت کننده")]

        public int UserId { get; set; }

        public User User { get; set; }
        public NewGroup NewGroup { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}