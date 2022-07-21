using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        public readonly iStudentRepository _studentrepository;
        public SchoolController(iStudentRepository studentrepository)
        {
            _studentrepository = studentrepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentrepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetBooks(int id)
        {
            return await _studentrepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostBooks([FromBody] Student student)
        {
            var newStudent = await _studentrepository.Create(student);
            return CreatedAtAction(nameof(GetBooks), new { id = newStudent.StudentId }, newStudent);
        }

        [HttpPut]
        public async Task<ActionResult> PutStudents(int id, [FromBody] Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
            await _studentrepository.update(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var studentToDelete = await _studentrepository.Get(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }
            await _studentrepository.Delete(studentToDelete.StudentId);
            return NoContent();
        }
    }
}
