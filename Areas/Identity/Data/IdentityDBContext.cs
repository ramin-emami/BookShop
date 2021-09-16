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
    public class IdentityDBContext : IdentityDbContext<BookShopUser,ApplicationRole,string,IdentityUserClaim<string>,ApplicationUserRole,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
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
        }
    }
}
