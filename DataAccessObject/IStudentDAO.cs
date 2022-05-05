using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public interface IStudentDAO
    {
        Task<List<Student>> GetAll();
        Task<Student> GetStudent(string studentName);
        Task AddStudent(string studentName);
    }
}
