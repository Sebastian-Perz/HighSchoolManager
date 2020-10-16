using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSchoolManager.ViewModels
{
    public class FilledCourseData
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public bool Filled { get; set; }
    }
}