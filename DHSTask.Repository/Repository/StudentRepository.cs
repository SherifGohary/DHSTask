using DHSTask.Data.Models;
using DHSTask.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHSTask.Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DbContext _dbContext;
        public StudentRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Student Add(Student student)
        {
            student.Id = Guid.NewGuid();
            _dbContext.Set<Student>().Add(student);
            try
            {
                _dbContext.SaveChanges();
                return student;
            }
            catch
            {
                throw new Exception("Student not added, please contact your adminstrator.");
            }
        }

        public bool Delete(Student student)
        {
            try
            {
                _dbContext.Set<Student>().Remove(student);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Student> GetAll()
        {
            var students = _dbContext.Set<Student>().ToList();
            return students;
        }

        public Student GetSingleById(Guid id)
        {
            var student = _dbContext.Set<Student>().SingleOrDefault(s => s.Id == id);
            if(student != null)
            {
                return student;
            }
            else
            {
                throw new Exception("Student not found");
            }
        }

        public Student Update(Student student)
        {
            try
            {
                Student oldStudent = _dbContext.Set<Student>().SingleOrDefault(s => s.Id == student.Id);
                oldStudent.Fname = student.Fname;
                oldStudent.Lname = student.Lname;

                _dbContext.Entry<Student>(oldStudent).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return oldStudent;
            }
            catch
            {
                throw new Exception("Student not Updated");
            }
        }
    }
}
