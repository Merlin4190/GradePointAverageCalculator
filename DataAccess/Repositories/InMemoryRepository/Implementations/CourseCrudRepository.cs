using DataAccess.InMemoryRepository.Repositories.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.InMemoryRepository.Repositories.Implementations
{
    public class CourseCrudRepository : ICourseCrudRepository
    {
        public bool Add(string courseName, int courseCode, byte courseUnit, int courseScore)
        {
            int rowCountBefore = this.RowCount();
            var course = new Course();
            course.CourseName = courseName;
            course.CourseCode = courseCode;
            course.CourseUnit = courseUnit;
            course.CourseScore = courseScore;
            InMemoryContext.Courses.Add(course);
            InFileContext.Write(course);

            int rowCountAfter = this.RowCount();
            if (rowCountAfter <= rowCountBefore) return false;
            return true;
        }

        public int RowCount()
        {
            return InMemoryContext.Courses.Count;
        }

        public async Task<List<Course>> Get()
        {
            return InMemoryContext.Courses;
            //return await InFileContext.ReadAsync();
        }

        public void Display()
        {
            foreach (var course in InMemoryContext.Courses)
            {
                string name = course.CourseName;
                int code = course.CourseCode;
                byte unit = course.CourseUnit;
                int score = course.CourseScore;
            }
        }

        public void Delete()
        {
            InFileContext.Delete();
        }
    }
}
