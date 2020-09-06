using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppBaoMing.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // ...
        }

        public DbSet<AdminItem> Users { get; set; }
        public DbSet<XueKe> XueKe   { get; set; }
        public DbSet<GangWei> GangWei { get; set; }
        public DbSet<UserInfo> UserInfo{ get; set; }
    }
}
