using Services;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPAClient
{
    public static class App
    {

        //IHistory history = GlobalConfig.History;

        public static async Task Run()
        {
            UserClient user = new UserClient();
            char? choice;
            Console.WriteLine("********************** Welcome To Grade Point Average (GPA) Calculator **********************");

            while (true)
            {
                Console.WriteLine("\nInput '1' to calculate GPA\nInput '2' to view archive\nInput '3' to exit ");
                Console.Write("\nYour Input: ");
                choice = Console.ReadLine().ToCharArray()[0];
                if (choice == '1')
                {
                    Console.WriteLine("********************** Grade Point Average (GPA) Calculator **********************");
                    Console.WriteLine("\n********************** GuideLines **********************\n");
                    Console.WriteLine("1. Enter Number of Courses ");
                    Console.WriteLine("2. Enter The Course Name.");
                    Console.WriteLine("3. Enter The Course Code. The Course Code should contain three(3) numbers with no whitespace.");
                    Console.WriteLine("4. Enter The Course Unit. The Course Unit should be a number between 1 and 6");
                    Console.WriteLine("5. Enter The Course Score. The Course Score should be a number between 0 and 100\n");

                    //UserClient user = new UserClient();

                    await user.Register();

                    Console.WriteLine("Compiling and Calculating your GPA...");
                    await user.Output();
                }
                else if(choice == '2')
                {
                    Console.WriteLine("Retrieving Archive...");
                    Console.WriteLine("\n                                       ARCHIVE                                              ");

                    //user.history.RetrieveAsync
                    var lines = await GlobalConfig.History.RetrieveAsync();

                    foreach (var line in lines) Console.WriteLine(line);
                }
                else if (choice == '3')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Entry");
                }
            }
        }
    }
}
