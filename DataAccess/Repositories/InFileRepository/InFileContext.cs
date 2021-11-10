using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class InFileContext 
    {
        public static string filePath = @"C:\Users\Decagon\Documents\DECAGON\C#\PROJECTS\courses.txt";

        public static void Write(Course course)
        {
            string output = $"{course.CourseName}, {course.CourseCode}, {course.CourseUnit}, {course.CourseScore}";

            string newString = string.Join(",", output);
            try
            {
                if (File.Exists(filePath)) File.AppendAllText(filePath, newString + "\n");
                else File.WriteAllText(filePath, newString + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public static async Task<List<Course>> ReadAsync()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var lines = (await File.ReadAllLinesAsync(filePath)).ToList();
                    List<Course> courseList = new List<Course>();

                    foreach (var line in lines)
                    {
                        List<string> data = line.Split(',').ToList();
                        Course course = new Course();
                        course.CourseName = data[0];
                        course.CourseCode = Convert.ToInt32(data[1]);
                        course.CourseUnit = Convert.ToByte(data[2]);
                        course.CourseScore = Convert.ToInt32(data[3]);
                        courseList.Add(course);
                    }

                    return courseList;
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static void Delete()
        {
            File.Delete(filePath);
        }
    }
}
