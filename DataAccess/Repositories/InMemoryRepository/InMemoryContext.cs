using System;
using System.Collections.Generic;
using Models;

namespace DataAccess
{
    public class InMemoryContext
    {
        public static List<Course> Courses { get; set; } = new List<Course>();
    }
}
