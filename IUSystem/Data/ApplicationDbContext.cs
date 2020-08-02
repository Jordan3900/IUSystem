using IUSystem.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IUSystem.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Subject> Subjects { get; set; }
    }
}
