using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApplication.Models;

namespace PostApplication.Data
{
    public class PostApplicationContext : DbContext
    {
        public PostApplicationContext (DbContextOptions<PostApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PostApplication.Models.User> User { get; set; } = default!;

        public DbSet<PostApplication.Models.Courier> Courier { get; set; } = default!;

        public DbSet<PostApplication.Models.Package> Package { get; set; } = default!;
    }
}
