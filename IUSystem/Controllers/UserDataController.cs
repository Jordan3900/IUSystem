using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.DtoModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IUSystem.Constans;

namespace IUSystem.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserDataController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        async public Task<UserDataDTO> Get(string name)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == name);
            if (user == null)
            {
                return new UserDataDTO { IsAdmin = false, IsTeacher = false };
            }

            var role = await userManager.GetRolesAsync(user);
            var isAdmin = role.Contains(Constants.ADMIN_ROLE);
            var teacher = this.dbContext.Teachers.FirstOrDefault(x => x.UserId == user.Id);
            var userData = new UserDataDTO { IsAdmin = isAdmin, IsTeacher = teacher != null };

            return userData;
        }
    }
}