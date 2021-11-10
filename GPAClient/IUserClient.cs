using System.Threading.Tasks;

namespace GPAClient
{
    interface IUserClient
    {
        string InputCourseName();
        int InputCourseCode();
        byte InputCourseUnit();
        int InputCourseScore();

        Task Register();
    }
}
