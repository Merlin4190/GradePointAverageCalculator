using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IComputation
    {
        Task<bool> RegisterCourse(string CourseName, int CourseCode, byte CourseUnit, int CourseScore);
        Task<List<Course>> GetCourses();
        void DeleteFile();

        //List<Tuple<Course, char, byte>> GetCourses();
    }
}
