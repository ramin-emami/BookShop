using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser,ApplicationRole,string,IdentityUserClaim<string>,ApplicationUserRole,IdentityUserLogin<string>,ApplicationRoleClaim,IdentityUserToken<string>>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationRole>().ToTable("AspNetRoles").ToTable("AppRoles");

            builder.Entity<ApplicationUserRole>().ToTable("AppUserRole");

            builder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users).HasForeignKey(r => r.RoleId);

            builder.Entity<ApplicationUser>().ToTable("AppUsers");

            builder.Entity<ApplicationUserRole>()
               .HasOne(userRole => userRole.User)
               .WithMany(role => role.Roles).HasForeignKey(r => r.UserId);

            builder.Entity<ApplicationRoleClaim>().ToTable("AppRoleClaim");

            builder.Entity<ApplicationRoleClaim>()
                .HasOne(roleclaim => roleclaim.Role)
                .WithMany(claim => claim.Claims).HasForeignKey(c => c.RoleId);
        }
    }
}
