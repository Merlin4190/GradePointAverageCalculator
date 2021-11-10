using Commons;
using Services;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GPAClient
{
    class UserClient: IUserClient
    {
        IComputation compute = GlobalConfig.Computation;
        IGradingSystem grade = GlobalConfig.GradingSystem;
        ILoggerManager logger = GlobalConfig.LoggerManager;
        IHistory history = GlobalConfig.History;

        public string InputCourseName()
        {
            Console.Write("Please Enter Course Name- ");
            string courseName = Console.ReadLine();
            bool name = courseName.All(Char.IsLetter);           

            if (name)
            {
                courseName = Validate.ConvertToUppercase(courseName);
            }

            if (!name)
            {
                courseName = "";
                Console.WriteLine("Please Check The Course Name Again.");
                courseName = InputCourseName();
            }

            return courseName;
        }

        public int InputCourseCode()
        {
            Console.Write("Please Enter Course Code- ");
            int courseCode;
            bool code = Validate.IntNumericChecker(Console.ReadLine(), out courseCode);

            if (code == false)
            {
                Console.WriteLine("Please Check The Course Code Again. Course Code should be number.");               
                courseCode = InputCourseCode();
            }

            if (courseCode.ToString().Length != 3)
            {
                Console.WriteLine("Please Check The Course Code Again. Length should be 3.");
                courseCode = InputCourseCode();
            }

            return courseCode;
        }

        public byte InputCourseUnit()
        {
            Console.Write("Please Enter Course Unit- ");
            byte courseUnit;
            bool unit = Validate.ByteNumericChecker(Console.ReadLine(), out courseUnit);

            if (unit == false && courseUnit < 1 || courseUnit > 6)
            {
                Console.WriteLine("Please Check The Course Unit Again. Course Unit should be between 1 and 6.");
                courseUnit = InputCourseUnit();
            }

            return courseUnit;
        }

        public int InputCourseScore()
        {
            Console.Write("Please Enter the Score- ");
            int courseScore;
            bool score = Validate.IntNumericChecker(Console.ReadLine(), out courseScore);

            if (score == false || courseScore < 0 || courseScore > 100)
            {
                Console.WriteLine("Please Check The Course Score Again. Course Score should be between 0 and 100.");
                courseScore = InputCourseScore();
            }

            return courseScore;
        }

        public async Task Register()
        {
            bool end = false;
            Console.Write("Please Enter Total Number of Courses- ");
            int numberOfCourses;
            bool numberChecks = int.TryParse(Console.ReadLine(), out numberOfCourses);
            try
            {
                while(numberChecks == false)
                {
                    Console.WriteLine("Please check again, should be a number");
                    Console.Write("Please Enter Total Number of Courses- ");
                    numberChecks = int.TryParse(Console.ReadLine(), out numberOfCourses);                    
                }

                while (!end || numberOfCourses != 0)
                {
                    string name = InputCourseName();
                    int code = InputCourseCode();
                    byte unit = InputCourseUnit();
                    int score = InputCourseScore();

                    end = await compute.RegisterCourse(name, code, unit, score);
                    if (end) numberOfCourses--;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                //Console.WriteLine(ex.Message);
            }
            
        }

        public async Task Output()
        {
            var allGradedCourses = await compute.GetCourses();
            int totalQualityPoint = 0;
            int totalCourseUnit = 0;

            String header = @"
            |---------------------|--------------|----------|---------------|
            |   COURSE & CODE     | COURSE UNIT  |  GRADE   |   GRADE UNIT  |
            |---------------------|--------------|----------|---------------|
            ";

            string body = "", output = "";

            string footer = @"
            |---------------------|--------------|----------|---------------|
            ";

            foreach (var course in allGradedCourses)
            {
                char gradeType = grade.GradeType(course.CourseScore);
                byte gradeUnit = grade.GradeUnit(gradeType);
                int qualityPoint = grade.QualityPoint(course.CourseUnit, gradeUnit);
                totalQualityPoint += qualityPoint;
                totalCourseUnit = await grade.AccumulatedCourseUnit();

                body += $@"
            |     {course.CourseName} {course.CourseCode}         |     {course.CourseUnit}        |   {gradeType}      |      {gradeUnit}        |
";
            }            
            output = header + body + footer;
            Console.WriteLine(output);
            double gpa = grade.CalculateGPA(totalQualityPoint, totalCourseUnit);
            Console.WriteLine($"            Your GPA is = {gpa:0.00} to 2 decimal places");

            Console.ReadLine();

            //compute.DeleteFile();
            string archive = $"{output} \n            Your GPA is = {gpa:0.00} to 2 decimal places";
            history.Archive(archive);
        }
    }
}
