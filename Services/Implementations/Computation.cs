using DataAccess;
using DataAccess.InMemoryRepository.Repositories.Interfaces;
using Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class Computation : IComputation
    {
        public ICourseCrudRepository _courseRepository;
        public ILoggerManager _logger;

        public Computation(ICourseCrudRepository courseRepo, ILoggerManager logger)
        {
            _courseRepository = courseRepo;
            _logger = logger;
        }
        public async Task<bool> RegisterCourse(string CourseName, int CourseCode, byte CourseUnit, int CourseScore)
        {
            var course = await _courseRepository.Get();
            if (course != null && course.Where(x => x.CourseName == CourseName && x.CourseCode == CourseCode).Count() != 0)
            {
                return false;
            }
            else
            {
                var register = GlobalConfig.CourseCrudRepository;
                register.Add(CourseName, CourseCode, CourseUnit, CourseScore);
                return true;
            }
        }

        public async Task<List<Course>> GetCourses()
        {
            var course = await _courseRepository.Get();
            return course;
        }

        public void DeleteFile()
        {
            _courseRepository.Delete();
        }

    }
}
