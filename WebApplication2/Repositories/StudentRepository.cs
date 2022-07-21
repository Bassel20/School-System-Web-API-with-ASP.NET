using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class StudentRepository : iStudentRepository
    {

        private readonly SchoolDbContext _context;
        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Student> Create(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task Delete(int id)
        {
            var studentToDelete = await _context.Students.FindAsync(id);
            _context.Students.Remove(studentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> Get()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> Get(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task update(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
