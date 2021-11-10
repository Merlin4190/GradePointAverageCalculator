using Models;
using System;
using System.Collections.Generic;

namespace Data
{
    public class Database
    {
        private List<Course> _courses;

        public List<Course> Courses
        {
            get
            {
                if (_courses == null)
                {
                    _courses = new List<Course>();
                    return _courses;
                }
                return _courses;
            }
        }
    }
}
