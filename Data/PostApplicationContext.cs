using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApplication.Models;

namespace PostApplication.Data
{
    public class PostApplicationContext : DbContext
    {

        public PostApplicationContext(DbContextOptions<PostApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PostApplication.Models.User> User { get; set; } = default!;

        public DbSet<PostApplication.Models.Courier> Courier { get; set; } = default!;

        public DbSet<PostApplication.Models.Package> Package { get; set; } = default!;

        public DbSet<PostApplication.Models.PostOffice> PostOffice { get; set; } = default!;

        public DbSet<PostApplication.Models.Cashier> Cashier { get; set; } = default!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=post_app;Username=root;Password=root;");
            }
        }*/
    }
}
