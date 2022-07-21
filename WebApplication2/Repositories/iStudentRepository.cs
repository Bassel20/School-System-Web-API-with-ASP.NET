using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface iStudentRepository
    {
        Task<IEnumerable<Student>> Get();
        Task<Student> Get(int id);
        Task<Student> Create(Student student);
        Task update(Student student);
        Task Delete(int id);
    }
}
