using DHSTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHSTask.Repository.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetSingleById(Guid id);
        Student Add(Student student);
        Student Update(Student student);
        bool Delete(Student student);
    }
}
