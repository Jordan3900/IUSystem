using IUSystem.Data;
using IUSystem.Models;
using IUSystem.Constans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
           HttpContext context,
           ApplicationDbContext dbContext,
           UserManager<IdentityUser> userManager
         )
        {
            if (!dbContext.Teachers.Any())
            {
                await this.SeedTeachers(dbContext, userManager);
            }

            if (!dbContext.Students.Any())
            {
                await this.SeedStudents(dbContext, userManager);
            }

            if (!dbContext.Rooms.Any())
            {
                await this.SeedRooms(dbContext);
            }

            if (!dbContext.Subjects.Any())
            {
                await this.SeedSubjects(dbContext);
            }


            if (!dbContext.Lectures.Any())
            {
                await this.SeedLectures(dbContext);
            }

            await this.next(context);
        }

        async private Task SeedLectures(ApplicationDbContext dbContext)
        {
            var lectures = new List<Lecture>();
            var random = new Random();
            var teachers = dbContext.Teachers.ToList();
            var subjects = dbContext.Subjects.ToList();
            var rooms = dbContext.Rooms.ToList();

            for (int i = 0; i < teachers.Count; i++)
            {
                var teacherIndex = random.Next(0, 10);
                var startTime = DateTime.Now.AddHours(i + 2);
                var endTime = startTime.AddHours(3);
                var teacher = teachers[teacherIndex];
                var name = "Test Subject of Theory" + i;
                var subject = subjects.FirstOrDefault(x => x.Name == name);
                var roomNum = i + 1 + "00";
                var room = rooms.FirstOrDefault(x => x.Name == roomNum);
                var id = Guid.NewGuid().ToString();
                var lecture = new Lecture { Id = id, StartTime = startTime, EndTime = endTime, Room = room, Subject = subject, Teacher = teacher };

                dbContext.Lectures.Add(lecture);
                lectures.Add(lecture);
                teacher.Lectures.Add(lecture);
            }
            dbContext.Lectures.AddRange(lectures);
            await dbContext.SaveChangesAsync();
        }

        async private Task SeedSubjects(ApplicationDbContext dbContext)
        {
            var subjects = new List<Subject>();
            var random = new Random();
            var students = dbContext.Students.ToList();

            for (int i = 0; i < students.Count; i++)
            {
                var id = Guid.NewGuid().ToString();
                var studentIndex = random.Next(0, 10);
                var grade = random.Next(2, 7);
                var student = students[studentIndex];
                var name = "Test Subject of Theory" + i;

                var subject = new Subject { Id = id, Grade = grade, Student = student, Name = name };
                subjects.Add(subject);
            }

            dbContext.Subjects.AddRange(subjects);
            await dbContext.SaveChangesAsync();
        }

        async private Task SeedRooms(ApplicationDbContext dbContext)
        {
            var rooms = new List<Room>();

            for (int i = 1; i <= 10; i++)
            {
                var id = Guid.NewGuid().ToString();
                var room = new Room { Id = id, Name = i + "00" };
                rooms.Add(room);
            }

            dbContext.Rooms.AddRange(rooms);
            await dbContext.SaveChangesAsync();
        }

        async private Task SeedStudents(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            var students = new List<Student>();

            for (int i = 0; i < 10; i++)
            {
                var username = "StudentTest@test.bg" + i;
                var email = "StudentTest@test.bg" + i;
                var user = new IdentityUser { UserName = username, Email = email };
                var result = await userManager.CreateAsync(user, Constants.SEED_USERS_PASSWORD);
                var id = Guid.NewGuid().ToString();

                if (result.Succeeded)
                {
                    var student = new Student
                    {
                        Id = id,
                        FirstName = "Test" + i,
                        MiddleName = "Testov" + i,
                        LastName = "Testovski" + i,
                        User = user,
                        Number = "69696969696969" + i
                    };

                    students.Add(student);
                    await userManager.AddToRoleAsync(user, Constants.STUDENT_ROLE);
                }
            }

            dbContext.Students.AddRange(students);
            await dbContext.SaveChangesAsync();

        }

        async private Task SeedTeachers(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            var teachers = new List<Teacher>();

            for (int i = 0; i < 10; i++)
            {
                var user = new IdentityUser { UserName = "TeacherTest@test.bg" + i, Email = "TeacherTest@test.bg" + i };
                var result = await userManager.CreateAsync(user, Constants.SEED_USERS_PASSWORD);
                var id = Guid.NewGuid().ToString();

                if (result.Succeeded)
                {
                    var student = new Teacher
                    {
                        Id = id,
                        FirstName = "Teacher" + i,
                        MiddleName = "Teacherov" + i,
                        LastName = "Teachers" + i,
                        User = user,
                        Number = "69696969696969" + i
                    };

                    teachers.Add(student);
                    await userManager.AddToRoleAsync(user, Constants.TEACHER_ROLE);
                }
            }

            dbContext.Teachers.AddRange(teachers);
            await dbContext.SaveChangesAsync();
        }
    }
}
