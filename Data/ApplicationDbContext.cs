using BlazorServer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<PostCategory> PostCategory { get; set; }
        public DbSet<PostComment> PostComment { get; set; }
        public DbSet<Tag> Tag { get; set; }
    }
}
