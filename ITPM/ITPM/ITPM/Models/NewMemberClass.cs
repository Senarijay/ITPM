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

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name Should be Consists with letters")]
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

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Phone Number Should be Consists 10 Numbers")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter the Phone Number")]
        [Display(Name = "Phone Number")]
        public String Pnumber { get; set; }



    }
}
      
