using DataAccess.InMemoryRepository.Repositories.Implementations;
using DataAccess.InMemoryRepository.Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;

namespace Services
{
    public static class GlobalConfig
    {
        /// <summary>
        /// THIS IS A GLOBAL CONFIGURATION CLASS CREATED TO FACILITATE DEPENDENCY INJECTION. 
        /// </summary>

        public static ILoggerManager LoggerManager;
        public static IGradingSystem GradingSystem;
        public static IComputation Computation;
        public static ICourseCrudRepository CourseCrudRepository;
        public static IHistory History;

        static GlobalConfig()
        {
            LoggerManager = new LoggerManager();
            CourseCrudRepository = new CourseCrudRepository();
            GradingSystem = new GradingSystem(CourseCrudRepository, LoggerManager);
            Computation = new Computation(CourseCrudRepository, LoggerManager);
            History = new History(LoggerManager);
        }

        public static void Destroy()
        {
            GradingSystem = null;
            Computation = null;
            CourseCrudRepository = null;
            LoggerManager = null;
            History = null;
        }

        //public static IComputation CreateComputation()
        //{
        //    if (compute == null) compute = new Computation();
        //    return compute;
        //}

        //public static ICourseCrudRepository CourseCRUD()
        //{
        //    if (register == null) register = new CourseCrudRepository();
        //    return register;
        //}

        //public static IGradingSystem CreateGrade()
        //{
        //    if (grade == null) grade = new GradingSystem();
        //    return grade;
        //}
    }
}
