using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IUSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public TeacherController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            var teachers = dbContext.Teachers
                .Select(x => new Teacher
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                   
                }).ToArray();

            return teachers;
        }
    }
}