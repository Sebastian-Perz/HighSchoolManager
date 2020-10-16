namespace HighSchoolManager.Migrations
{
    using HighSchoolManager.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HighSchoolManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HighSchoolManager.Models.ApplicationDbContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander",
                    BirthDate = DateTime.Parse("1991-09-01"),
                    AssignmentDate = DateTime.Parse("2008-09-01") },
                new Student { FirstName = "Ceme",   LastName = "Rama",
                    BirthDate = DateTime.Parse("1992-09-01"),
                    AssignmentDate = DateTime.Parse("2017-09-01") },
                new Student { FirstName = "Ramroll",   LastName = "Mona",
                    BirthDate = DateTime.Parse("1993-09-01"),
                    AssignmentDate = DateTime.Parse("2016-09-01") },
                new Student { FirstName = "Serwisene",   LastName = "Amir",
                    BirthDate = DateTime.Parse("1994-09-01"),
                    AssignmentDate = DateTime.Parse("2015-09-01") },
                new Student { FirstName = "Comet",   LastName = "Alibaba",
                    BirthDate = DateTime.Parse("1995-09-01"),
                    AssignmentDate = DateTime.Parse("2014-09-01") },
                new Student { FirstName = "Fagon",   LastName = "Anand",
                    BirthDate = DateTime.Parse("1996-09-01"),
                    AssignmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Ximilli",   LastName = "Bepo",
                    BirthDate = DateTime.Parse("1997-09-01"),
                    AssignmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Hime",   LastName = "Mbappe",
                    BirthDate = DateTime.Parse("1998-09-01"),
                    AssignmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Taylor",   LastName = "Swift",
                    BirthDate = DateTime.Parse("1999-09-01"),
                    AssignmentDate = DateTime.Parse("2010-09-01") },
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var professors = new List<Professor>
            {
                new Professor { FirstName = "Kim",     LastName = "Abercrombie",
                    BirthDate = DateTime.Parse("1990-09-01"),
                    HireDate = DateTime.Parse("1995-03-11") },
                new Professor { FirstName = "Fadi",    LastName = "Fakhouri",
                    BirthDate = DateTime.Parse("1990-09-01"),
                    HireDate = DateTime.Parse("2002-07-06") },
                new Professor { FirstName = "Roger",   LastName = "Harui",
                    BirthDate = DateTime.Parse("1990-09-01"),
                    HireDate = DateTime.Parse("1998-07-01") },
                new Professor { FirstName = "Candace", LastName = "Kapoor",
                    BirthDate = DateTime.Parse("1990-09-01"),
                    HireDate = DateTime.Parse("2001-01-15") },
                new Professor { FirstName = "Roger",   LastName = "Zheng",
                    BirthDate = DateTime.Parse("1990-09-01"),
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            professors.ForEach(s => context.Professors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var divisions = new List<Division>
            {
                new Division { Name = "English",
                    ProfessorID  = professors.Single( i => i.LastName == "Abercrombie").ID },
                new Division { Name = "Mathematics",
                    ProfessorID  = professors.Single( i => i.LastName == "Fakhouri").ID },
                new Division { Name = "Engineering",
                    ProfessorID  = professors.Single( i => i.LastName == "Harui").ID },
                new Division { Name = "Economics",
                    ProfessorID  = professors.Single( i => i.LastName == "Kapoor").ID }
            };
            divisions.ForEach(s => context.Divisions.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",
                  DivisionID = divisions.Single( s => s.Name == "Engineering").DivisionID,
                  Professors = new List<Professor>()
                },
                new Course {CourseID = 4022, Title = "Microeconomics",
                  DivisionID = divisions.Single( s => s.Name == "Economics").DivisionID,
                   Professors = new List<Professor>()
                },
                new Course {CourseID = 4041, Title = "Macroeconomics",
                  DivisionID = divisions.Single( s => s.Name == "Economics").DivisionID,
                   Professors = new List<Professor>()
                },
                new Course {CourseID = 1045, Title = "Calculus",
                 DivisionID = divisions.Single( s => s.Name == "Mathematics").DivisionID,
                  Professors = new List<Professor>()
                },
                new Course {CourseID = 3141, Title = "Trigonometry",
                  DivisionID = divisions.Single( s => s.Name == "Mathematics").DivisionID,
                  Professors = new List<Professor>()
                },
                new Course {CourseID = 2021, Title = "Composition",
                  DivisionID = divisions.Single( s => s.Name == "English").DivisionID,
                  Professors = new List<Professor>()
                },
                new Course {CourseID = 2042, Title = "Literature",
                  DivisionID = divisions.Single( s => s.Name == "English").DivisionID,
                   Professors = new List<Professor>()
                }
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    ProfessorID = professors.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    ProfessorID  = professors.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    ProfessorID  = professors.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" }
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.ProfessorID, s));
            context.SaveChanges();

            AddOrUpdateProfessor(context, "Chemistry", "Kapoor");
            AddOrUpdateProfessor(context, "Chemistry", "Harui");
            AddOrUpdateProfessor(context, "Microeconomics", "Zheng");
            AddOrUpdateProfessor(context, "Macroeconomics", "Zheng");

            AddOrUpdateProfessor(context, "Calculus", "Fakhouri");
            AddOrUpdateProfessor(context, "Trigonometry", "Harui");
            AddOrUpdateProfessor(context, "Composition", "Abercrombie");
            AddOrUpdateProfessor(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var assignments = new List<Assignment>
            {
                new Assignment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Mbappe").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Assignment {
                     StudentID = students.Single(s => s.LastName == "Bepo").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Assignment {
                     StudentID = students.Single(s => s.LastName == "Bepo").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Bepo").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.F
                 },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Assignment {
                    StudentID = students.Single(s => s.LastName == "Alibaba").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Rama").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Assignment {
                    StudentID = students.Single(s => s.LastName == "Mona").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };

            foreach (Assignment e in assignments)
            {
                var assignmentInDataBase = context.Assignments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.CourseID == e.CourseID).FirstOrDefault();
                if (assignmentInDataBase == null)
                {
                    context.Assignments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateProfessor(ApplicationDbContext context, string courseTitle, string professorName)
        {
            var crs = context.Courses.FirstOrDefault(c => c.Title == courseTitle);
            var inst = crs.Professors.FirstOrDefault(i => i.LastName == professorName);
            if (inst == null)
                crs.Professors.Add(context.Professors.FirstOrDefault(i => i.LastName == professorName));
        }
    }
    
}
