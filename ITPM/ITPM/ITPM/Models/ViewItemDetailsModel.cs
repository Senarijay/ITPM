using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITPM.Models
{
    public class ViewItemDetailsModel
    {
        public int CoachID { get; set; }

        public string Cname { get; set; }
        public int CAge { get; set; }
        public int contactnumber { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }

    }
}
