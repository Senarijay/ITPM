using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITPM.Models
{
    public class NewMemberClass
    {
        [Key]
        public int Mid { get; set; }

        [Required(ErrorMessage ="Enter Member Name")]
        [Display(Name = "Member Name")]
        public String Mname { get; set; }

        [Required(ErrorMessage ="Enter Age of Member")]
        [Display(Name = "Age" )]
        [Range(20,45)]
        public int Age { get; set; }
        
        [Required(ErrorMessage ="Enter Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Enter the Address")]
        [Display(Name = "Address")]
        public String MAddress { get; set; }

        [Required(ErrorMessage = "Enter the Phone Number")]
        [Display(Name = "Phone Number")]
        [StringLength(10)]
        public String Pnumber { get; set; }



    }
}
      
