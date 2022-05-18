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

        [Required(ErrorMessage = "Enter CoachName")]
        [Display(Name = "Coach Name")]
        public string Cname { get; set; }

        [Required(ErrorMessage = "Enter Age")]
        [Display(Name = "Age")]
        public int CAge{ get; set; }

        [Required(ErrorMessage = "Enter Contact Number")]
        [Display(Name = "Contact Number")]
        public int contactnumber { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        [Display(Name = "Description")]
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
