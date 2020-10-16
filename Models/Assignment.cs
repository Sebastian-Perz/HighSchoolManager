using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HighSchoolManager.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }
    public class Assignment
    {
        public int AssignmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }


        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}