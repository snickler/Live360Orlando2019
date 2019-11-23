using IdentityDemo_Scaffolding.Models;
using Microsoft.EntityFrameworkCore;


namespace IdentityDemo_Scaffolding.Data
{
    public class MFATokenDbContext : DbContext
    {
        public DbSet<UserToken> UserTokens { get; set; }

        public MFATokenDbContext()
        {

        }
        public MFATokenDbContext(DbContextOptions<MFATokenDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToken>(builder =>
            {
                builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
