using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HighSchoolManager.Models
{
    public class Division
    {
        public int DivisionID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Division")]
        public string Name { get; set; }
        [Display(Name = "Professor's ID")]
        public int? ProfessorID { get; set; }
        [Display(Name = "Division's Headmaster")]
        public virtual Professor Headmaster { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}