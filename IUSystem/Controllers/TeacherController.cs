using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.DtoModels;
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
        public IEnumerable<TeacherDTO> Get()
        {
            var teachers = dbContext.Teachers
                .Select(x => new TeacherDTO
                {
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    UserId = x.UserId,
                    Id = x.Id,
                    Lectures = x.Lectures.Select(x => x.Subject.Name).ToArray(),
                    Number = x.Number,
                    LastName = x.LastName,

                }).ToArray();

            return teachers;
        }
    }
}