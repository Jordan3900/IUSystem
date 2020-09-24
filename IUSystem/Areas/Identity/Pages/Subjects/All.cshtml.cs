using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IUSystem.Areas.Identity.Pages.Subjects
{
    [Authorize(Roles = "Admin")]
    public class AllModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public AllModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Subjects = this.dbContext.Subjects.ToList();
        }

        public IList<Subject> Subjects { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            var subject = await this.dbContext.Subjects.FindAsync(id);

            if (subject != null)
            {
                this.dbContext.Subjects.Remove(subject);
                await this.dbContext.SaveChangesAsync();
            }
            this.Subjects = this.dbContext.Subjects.ToList();

            return Page();
        }
    }
}