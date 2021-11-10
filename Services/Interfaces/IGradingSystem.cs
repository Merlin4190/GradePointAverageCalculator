using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGradingSystem
    {
        char GradeType(int courseScore);
        byte GradeUnit(char gradeType);

        int QualityPoint(byte CourseUnit, byte gradeUnit);
        //int TotalQualityPoint(int qualityPoint);
        Task<int> AccumulatedCourseUnit();
        double CalculateGPA(double totalQualityPoint, double totalCourseUnit);
    }
}
