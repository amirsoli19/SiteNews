using CMSNewModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class UserViewModel
    {
        [Display(Name ="کد کاربر")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "نام کاربری")]

        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت کاربر")]

        public DateTime RegisterDate { get; set; }
        [Required]
        [Display(Name = "وضعیت کاربر")]

        public bool IsActive { get; set; }

        public virtual ICollection<News> Newses { get; set; }
    }
}