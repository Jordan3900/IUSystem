using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IUSystem.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UserDataController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public UserDataDTO Get(string name)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == name);
            if (user == null)
            {
                return new UserDataDTO { IsAdmin = false, IsTeacher = false };
            }
            var isAdmin = this.dbContext.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            var teacher = this.dbContext.Teachers.FirstOrDefault(x => x.UserId == user.Id);
            var userData = new UserDataDTO { IsAdmin = isAdmin != null, IsTeacher = teacher != null };


            return userData;
        }
    }
}