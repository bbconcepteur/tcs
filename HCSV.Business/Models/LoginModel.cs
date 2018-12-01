using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSV.Business.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.GlobalResource), ErrorMessageResourceName = "USERNAME_REQUIRED")]
        [Display(Name = "TEN_DANG_NHAP", ResourceType = typeof(Resources.GlobalResource))]
        public  string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.GlobalResource), ErrorMessageResourceName = "PASSWORD_REQUIRED")]
        [Display(Name = "MAT_KHAU", ResourceType = typeof(Resources.GlobalResource))]
        public  string Password { get; set; }

        public int MenuId { get; set; }
    }
}
