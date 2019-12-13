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
    public class TeacherDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public TeacherDetailsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<TeacherDetailsDTO> Get()
        {
            var teachers = dbContext.Teachers
                .Select(x => new TeacherDetailsDTO
                {
                    Name = x.FirstName + " " + x.MiddleName + " " + x.LastName, 
                    Id = x.Id,
                    Lectures = x.Lectures.Select(l => new LecturesDTO
                    {
                        Room = l.Room.Name,
                        EndTime = l.EndTime.ToString("HH:mm"),
                        StartTime = l.StartTime.ToString("HH:mm"),
                        Date = l.StartTime.ToString("MM/dd/yyyy"),
                        Subject = l.Subject.Name
                    }).ToArray(),
                    Number = x.Number,
                    Email = x.User.Email
                }).ToArray();

            return teachers;
        }
    }
}