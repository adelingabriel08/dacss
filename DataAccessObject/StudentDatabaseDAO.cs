using DataAccessObject.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class StudentDatabaseDAO : IStudentDAO
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentDatabaseDAO(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddStudent(string studentName)
        {
            await _dbContext.Students.AddAsync(new Student { Name = studentName.Trim() });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAll()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudent(string studentName)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(s => s.Name == studentName);
        }
    }
}
