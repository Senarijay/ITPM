using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITPM.Models;
using Microsoft.EntityFrameworkCore;

namespace ITPM.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<CoachClass> coaches { get; set; }

        public DbSet<FeedbackClass> FeedbackTable { get; set; }

        public DbSet<NewMemberClass> MemberTable { get; set; }
    }
}
