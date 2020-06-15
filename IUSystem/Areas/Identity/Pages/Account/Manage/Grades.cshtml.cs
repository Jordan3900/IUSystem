using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IUSystem.Areas.Identity.Pages.Account.Manage
{
    public class GradesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext dbContext;
        public GradesModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger,
            ApplicationDbContext dbContext
            )
        {
            this.dbContext = dbContext;
            _userManager = userManager;
        }

        public List<Subject> Subjects { get; set; }

        public async Task<IActionResult> OnGet()
        {
            
            var user = await _userManager.GetUserAsync(User);
            var student = this.dbContext.Students.FirstOrDefault(x => x.UserId == user.Id);

            if (student == null)
            {
                return RedirectToPage("../AccessDenied");
            }

          
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            this.Subjects = student.Subjects.ToList();

            return Page();
        }
    }
}