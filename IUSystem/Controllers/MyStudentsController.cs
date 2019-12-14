using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace IUSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyStudentsDataController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;


        public MyStudentsDataController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        [HttpGet]
        public IEnumerable<StudentDTO> Get(string name)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == name);

            if (user == null)
            {
                return new List<StudentDTO>();
            }

            var teacher = dbContext.Teachers.FirstOrDefault(x => x.UserId == user.Id);

            if (teacher == null)
            {
                RedirectToPage("./accessdenied");
                return new List<StudentDTO>();

            }

            var students = teacher.Lectures.Select(x => new StudentDTO 
            {
                Name = x.Subject.Student.FirstName + " " + x.Subject.Student.MiddleName + " " + x.Subject.Student.LastName,
                Number = x.Subject.Student.Number,
                Subject = x.Subject.Name,
                Grade = x.Subject.Grade
            });

            return students;
        }
    }
}