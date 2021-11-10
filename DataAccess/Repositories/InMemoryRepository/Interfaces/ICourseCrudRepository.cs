using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.InMemoryRepository.Repositories.Interfaces
{
    public interface ICourseCrudRepository
    {
        public bool Add(string courseName, int courseCode, byte courseUnit, int courseScore);
        //public List<Course> Get();
        public Task<List<Course>> Get();
        public void Display();
        public void Delete();

        public int RowCount();
    }
}
