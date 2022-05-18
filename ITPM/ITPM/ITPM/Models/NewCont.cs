using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITPM.Models
{
    public class NewCont
    {
        [Key]
        public int Cid { get; set; }

        //FirstName
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name Should be Consists with letters")]
        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "Fisrt Name")]
        public string Fname { get; set; }

        //LastName
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name Should be Consists with letters")]
        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        //Email
        [Required(ErrorMessage = "Enter Email address")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Phonenumber
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Phone Number Should be Consists 10 Numbers")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter Phone Number")]
        [Display(Name = "Phone Number")]
        public string Phonen { get; set; }

        //Message
        [Required(ErrorMessage = "Enter Message")]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Cmessage { get; set; }
    }
}
