using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="موبایل ")]
        public string MobileNumber { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا بخاطر بسپار")]

        public bool RemmemberPassword { get; set; }


        public string ReturnUrl { get; set; }
    }
}