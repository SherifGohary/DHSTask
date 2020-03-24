using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DHSTask.Data.Models;
using DHSTask.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DHSTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        public StudentsController(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }

        [HttpGet]
        [Route("All")]
        public IActionResult GetStudents()
        {
            var students = studentRepository.GetAll();
            return Ok(students);
        }

        //[HttpGet]
        //public IActionResult GetStudent(Guid id)
        //{
        //    var result = studentRepository.GetSingleById(id);
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("Add")]
        public IActionResult AddStudent(Student student)
        {
            if(student.Fname == string.Empty || student.Lname == string.Empty)
            {
                return BadRequest("Invalid Request");
            }

            try
            {
                var result = studentRepository.Add(student);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult EditStudent(Student student)
        {
            if (student.Fname == string.Empty || student.Lname == string.Empty)
            {
                return BadRequest("Invalid Request");
            }

            try
            {
                var result = studentRepository.Update(student);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteStudent(Guid id)
        {
            try
            {
                var result = studentRepository.Delete(new Student { Id = id });
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}