using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HighSchoolManager.Models;
using HighSchoolManager.ViewModels;

namespace HighSchoolManager.Controllers
{
    [Authorize]
    public class ProfessorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Professor
        public ActionResult Index(int? id, int? courseID)
        {

            var viewModel = new ProfessorIndexData();
            viewModel.Professors = db.Professors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Division))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.ProfessorID = id.Value;
                viewModel.Courses = viewModel.Professors.Where(
                    i => i.ID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                viewModel.Assignments = viewModel.Courses.Where(
                    x => x.CourseID == courseID).Single().Assignments;
            }

            return View(viewModel);
        }

        // GET: Professor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            var professor = new Professor();
            professor.Courses = new List<Course>();
            FillInCourseData(professor);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,BirthDate,HireDate,OfficeAssignment")] Professor professor, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                professor.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    professor.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillInCourseData(professor);
            return View(professor);
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single();
            FillInCourseData(professor);
            if (professor == null)
            {
                return HttpNotFound();
            }

            return View(professor);
        }

        private void FillInCourseData(Professor professor)
        {
            var allCourses = db.Courses;
            var professorCourses = new HashSet<int>(professor.Courses.Select(c => c.CourseID));
            var viewModel = new List<FilledCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new FilledCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Filled = professorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }


        // POST: Professor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var professorToUpdate = db.Professors
               .Include(i => i.OfficeAssignment)
               .Include(i => i.Courses)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(professorToUpdate, "",
               new string[] { "LastName", "FirstName", "BirthDate", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(professorToUpdate.OfficeAssignment.Location))
                    {
                        professorToUpdate.OfficeAssignment = null;
                    }

                    UpdateProfessorCourses(selectedCourses, professorToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            FillInCourseData(professorToUpdate);
            return View(professorToUpdate);
        }

        private void UpdateProfessorCourses(string[] selectedCourses, Professor professorToUpdate)
        {
            if (selectedCourses == null)
            {
                professorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var professorCourses = new HashSet<int>
                (professorToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!professorCourses.Contains(course.CourseID))
                    {
                        professorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (professorCourses.Contains(course.CourseID))
                    {
                        professorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
        // GET: Professor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professors
              .Include(i => i.OfficeAssignment)
              .Where(i => i.ID == id)
              .Single();
            db.Professors.Remove(professor);

            var division = db.Divisions
                .Where(d => d.ProfessorID == id)
                .SingleOrDefault();
            if (division != null)
            {
                division.ProfessorID = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
