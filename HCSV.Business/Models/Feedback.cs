using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSV.Business.Models
{
    public class Feedback
    {
        [Required(ErrorMessageResourceType = typeof(Resources.GlobalResource), ErrorMessageResourceName = "NAME_REQUIRED")]
        [Display(Name = "HO_TEN", ResourceType = typeof(Resources.GlobalResource))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.GlobalResource), ErrorMessageResourceName = "EMAIL_REQUIRED")]
        [Display(Name = "DIA_CHI_EMAIL", ResourceType = typeof(Resources.GlobalResource))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.GlobalResource), ErrorMessageResourceName = "SUBJECT_REQUIRED")]
        [Display(Name = "CHU_DE", ResourceType = typeof(Resources.GlobalResource))]
        public string Subject { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.GlobalResource), ErrorMessageResourceName = "CONTENT_REQUIRED")]
        [Display(Name = "NOI_DUNG", ResourceType = typeof(Resources.GlobalResource))]
        public string Message { get; set; }

    }
}
