using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHSTask.Data.Models
{
    public class StudentContext :DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options):base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
