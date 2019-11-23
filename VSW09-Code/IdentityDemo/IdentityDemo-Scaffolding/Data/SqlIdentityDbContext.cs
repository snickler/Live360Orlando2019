using IdentityDemo_Scaffolding.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo_Scaffolding.Data
{
    
        /// <summary>
        /// Maps AspNetMembership Database to a DBContext
        /// </summary>
        public class SqlIdentityDbContext : DbContext
        {


            public DbSet<SqlIdentityUser> Users { get; set; }
            public DbSet<SqlIdentityRole> Roles { get; set; }
            public DbSet<AspNetRoleClaims> RoleClaims { get; set; }
            public DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; }
            public DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }

            public DbSet<AspNetUser> NetUsers { get; }
            private DbSet<AspNetUsersInRoles> NetRoles { get; }
            private DbSet<AspNetMembership> AspNet_Memberships { get; }
            private DbSet<AspNetRoles> DBRoles { get; }
            private DbSet<AspNetProfile> AspNet_Profiles { get; }
            public SqlIdentityDbContext()
            {

            }

            public SqlIdentityDbContext(DbContextOptions<SqlIdentityDbContext> options) : base(options)
            {

            }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Model.RemoveEntityType(typeof(IdentityRoleClaim<Guid>));
            modelBuilder.Model.RemoveEntityType(typeof(IdentityRoleClaim<string>));
            modelBuilder.Model.RemoveEntityType(typeof(IdentityUserRole<Guid>));
            modelBuilder.Model.RemoveEntityType(typeof(SqlIdentityRole));
            modelBuilder.Model.AddEntityType(typeof(SqlIdentityRole));
            modelBuilder.Model.AddEntityType(typeof(IdentityRoleClaim<Guid>));

            modelBuilder.Entity<AspNetUser>()
                       .ToTable("aspnet_Users")
                        .HasKey(x => x.UserId);


            modelBuilder.Entity<AspNetRoles>()
                        .ToTable("aspnet_Roles")
                        .HasKey(x => x.RoleId);

            modelBuilder.Entity<AspNetUsersInRoles>()
                    .ToTable("aspnet_UsersInRoles")
                    .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<AspNetMembership>()
                .ToTable("aspnet_Membership")
                .HasKey(x => new { x.UserId, x.ApplicationId });


            modelBuilder.Entity<AspNetProfile>()
                .ToTable("aspnet_Profile")
                .HasKey(x => x.UserId);


            modelBuilder.Entity<SqlIdentityRole>()
                .HasNoKey()
                .ToQuery(() => DBRoles.Select(role => new SqlIdentityRole
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = role.RoleId,
                    Name = role.RoleName,
                    NormalizedName = role.RoleName.ToUpper()
                }));

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                    .HasNoKey()
                    .ToQuery(() => NetRoles.Select(userRole => new IdentityUserRole<Guid>
                    {

                        RoleId = userRole.RoleId,
                        UserId = userRole.UserId

                    }));


            modelBuilder.Entity<IdentityRoleClaim<Guid>>()
                .HasNoKey()
                .ToQuery(() => DBRoles.Select(roleClaim => new IdentityRoleClaim<Guid>
                {
                    RoleId = roleClaim.RoleId,
                    ClaimType = "Role",
                    ClaimValue = roleClaim.RoleName
                }).DefaultIfEmpty());



            modelBuilder.Entity<SqlIdentityUser>()
                    .HasNoKey()
                    .ToQuery(() => NetUsers.Join(AspNet_Memberships,
                     user => user.UserId,
                     membership => membership.UserId,
                     (user, membership) => new { User = user, Membership = membership })
                     .Join(AspNet_Profiles,
                     user => user.User.UserId,
                     profile => profile.UserId,
                     (user, profile) => new { ProfileUser = user, Profile = profile })
                    .Select(x => new SqlIdentityUser
                    {
                        Id = x.ProfileUser.Membership.UserId,
                        UserName = x.ProfileUser.User.UserName,
                        PasswordHash = (x.ProfileUser.Membership.Password + "|" + x.ProfileUser.Membership.PasswordFormat + "|" + x.ProfileUser.Membership.PasswordSalt),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Profile = x.Profile,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = true,
                        LockoutEnd = x.ProfileUser.Membership.LastLockoutDate,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        ApplicationId = x.ProfileUser.User.ApplicationId,
                        NormalizedUserName = x.ProfileUser.User.LoweredUserName,
                        LastActivityDate = x.ProfileUser.User.LastActivityDate,
                        Email = x.ProfileUser.Membership.Email,
                        NormalizedEmail = x.ProfileUser.Membership.LoweredEmail,
                    }));


            modelBuilder.Entity<AspNetUserRoles>()
                .HasNoKey()
                .ToQuery(() => NetRoles.Select(m => new AspNetUserRoles { }));



            modelBuilder.Entity<AspNetRoleClaims>()
                .HasNoKey()
                .ToQuery(() => NetRoles.Select(m => new AspNetRoleClaims()));

        }

        }
    }
