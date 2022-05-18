using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace ITPM.Models
{
    public class FeedbackClass
    {
        [Key]
        public int FeedID { get; set; }

        //FirstName
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Wrong Name")]
        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "Fisrt Name")]
        public String FName { get; set; }

        //LastName
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Wrong Name")]
        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        public String LName { get; set; }

        //Type
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Wrong Feedback Type")]
        [Required(ErrorMessage = "Enter Feedback Type")]
        [Display(Name = "Feedback Type")]
        public String FeedType { get; set; }

        //Derscription
        [Required(ErrorMessage = "Enter Feedback Comment")]
        [Display(Name = "Feedback Comment")]
        [DataType(DataType.MultilineText)]
        public String FeedDes { get; set; }

        [Display(Name = "Rate quality of our coach list:")]
        public int St_01 { get; set; }

        [Required(ErrorMessage = "Add Stars")]
        [Display(Name = "Rate quality of customer service:")]
        public int St_02 { get; set; }

        [Required(ErrorMessage = "Add Stars")]
        [Display(Name = "Rate overall experience with us:")]
        public int Rate { get; set; }

    }
}
