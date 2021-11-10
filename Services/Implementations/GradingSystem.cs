using System;
using DataAccess.InMemoryRepository.Repositories.Interfaces;
using Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    
    public class GradingSystem : IGradingSystem
    {
        public ICourseCrudRepository _courseRepository;
        public ILoggerManager _logger;

        public GradingSystem(ICourseCrudRepository courseRepo, ILoggerManager logger)
        {
            _courseRepository = courseRepo;
            _logger = logger;
        }

        public char GradeType(int courseScore)
        {
            char gradeType;

            if (courseScore <= 100 && courseScore >= 70)
            {
                gradeType = Grades.A.ToString()[0];
            }
            else if (courseScore <= 69 && courseScore >= 60)
            {
                gradeType = Grades.B.ToString()[0];
            }
            else if (courseScore <= 59 && courseScore >= 50)
            {
                gradeType = Grades.C.ToString()[0];
            }
            else if (courseScore <= 49 && courseScore >= 45)
            {
                gradeType = Grades.D.ToString()[0];
            }
            else if (courseScore <= 44 && courseScore >= 40)
            {
                gradeType = Grades.E.ToString()[0];
            }
            else
            {
                gradeType = Grades.F.ToString()[0];
            }
            return gradeType;
        }

        public byte GradeUnit(char gradeType)
        {
            byte gradeUnit = 0;

            if (gradeType.ToString() == nameof(Grades.A)) gradeUnit = (byte)Grades.A;
            else if (gradeType.ToString() == nameof(Grades.B)) gradeUnit = (byte)Grades.B;
            else if (gradeType.ToString() == nameof(Grades.C)) gradeUnit = (byte)Grades.C;
            else if (gradeType.ToString() == nameof(Grades.D)) gradeUnit = (byte)Grades.D;
            else if (gradeType.ToString() == nameof(Grades.E)) gradeUnit = (byte)Grades.E;
            else gradeUnit = (byte)Grades.F;

            return gradeUnit;
        }

        public int QualityPoint(byte courseUnit, byte gradeUnit)
        {
            int qualityPoint = courseUnit * gradeUnit;
            return qualityPoint;
        }

        public async Task<int> AccumulatedCourseUnit()
        {
            List<Course> courses = await _courseRepository.Get();
            int totalCourseUnit = 0;

            foreach (var course in courses)
            {
                totalCourseUnit += course.CourseUnit;
            }

            return totalCourseUnit;
        }

        public double CalculateGPA(double totalQualityPoint, double totalCourseUnit)
        {
            try
            {
                double gpa = 0.0;

                gpa = Math.Round((double)totalQualityPoint / totalCourseUnit, 2);

                return gpa;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                //Console.WriteLine(ex.Message);
            }
            return 0;

        }
    }

    enum Grades
    {
        A = 5,
        B = 4,
        C = 3,
        D = 2,
        E = 1,
        F = 0
    }


}
