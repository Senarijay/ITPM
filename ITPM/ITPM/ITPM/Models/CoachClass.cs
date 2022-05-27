using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ITPM.Models
{
    public class CoachClass
    {
        [Key]

        public int CoachID { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name Should be Consists with letters")]
        [Required(ErrorMessage = "Enter CoachName")]
        [Display(Name = "Coach Name")]
        public string Cname { get; set; }

        [Required(ErrorMessage = "Enter Age")]
        [Display(Name = "Age")]
        public int CAge{ get; set; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Phone Number Should be Consists 10 Numbers")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Enter Contact Number")]
        [Display(Name = "Contact Number")]
        public int contactnumber { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Add Stars")]
        [Display(Name = "Ratings")]
        public int Rate { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
