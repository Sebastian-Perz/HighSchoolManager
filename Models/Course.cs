using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HighSchoolManager.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public int DivisionID { get; set; }

        public virtual Division Division { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }
    }
}