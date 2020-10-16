using HighSchoolManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSchoolManager.ViewModels
{
    public class CourseIndexData
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
    }
}