using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HighSchoolManager.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Professor")]
        public int ProfessorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Professor Professor { get; set; }
    }
}