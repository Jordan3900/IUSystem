using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IUSystem.Areas.Identity.Pages.Lectures
{
    public class LecturesAddModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public LecturesAddModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Teachers = this.dbContext.Teachers.OrderBy(x => x.FirstName).ToList();
            this.Rooms = this.dbContext.Rooms.OrderBy(x => x.Name).ToList();
            this.Subjects = this.dbContext.Subjects.OrderBy(x => x.Name).ToList();
        }

        [Required]
        [BindProperty]
        [Display(Name = "Subject")]
        public string SubjectId { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Start Time")]
        public DateTime StartTime{ get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "End Time")]
        public DateTime EndTime{ get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Room")]
        public string RoomId { get; set; }

        [BindProperty]
        public ICollection<Teacher> Teachers{ get; set; }

        [BindProperty]
        public ICollection<Subject> Subjects { get; set; }

        [BindProperty]
        public ICollection<Room> Rooms { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var room = this.dbContext.Rooms.FirstOrDefault(x => x.Id == this.RoomId);
            var teacher = this.dbContext.Teachers.FirstOrDefault(x => x.Id == this.TeacherId);
            var subject = this.dbContext.Subjects.FirstOrDefault(x => x.Id == this.SubjectId);

            var lecture = new Lecture
            {
                Id = Guid.NewGuid().ToString(),
                EndTime = this.EndTime,
                StartTime = this.StartTime,
                Teacher = teacher,
                Room = room,
                Subject = subject
            };

            this.dbContext.Lectures.Add(lecture);
            this.dbContext.SaveChanges();

            return RedirectToPage("./LecturesAll"); ;
        }
    }
}
