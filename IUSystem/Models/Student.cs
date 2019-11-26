﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.Models
{
    public class Student 
    {
        public Student()
        {
            this.Lectures = new HashSet<Lectures>();
        }

        [Key]
        public string StudentId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public ICollection<Lectures> Lectures { get; set; }
    }
}
