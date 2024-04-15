using CodeBlog.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Data
{
    public class CodeBlogDbContext : IdentityDbContext
    {
        public CodeBlogDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags  { get; set; }

        public  DbSet<BlogPostLike> BlogPostLike { get; set; }

        public DbSet<BlogPostComment> BlogPostComment { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed the roles(user, admin, superadmin)
            var adminRoleId = "96ee7040-dbcd-44d4-a18f-d8b58eb3206f";
            var superAdminRoleId = "70e9f6d7-14fb-4221-9dc4-45fe2167cf0c";
            var userRoleId = "db2309f0-c714-498f-9c34-2684143f3929";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id= adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id= superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            //seed superadmin user
            var superAdminId = "a892b811-80bd-4e68-b3b7-0df95a0de725";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "superadmin@gmail.com".ToUpper(),
                NormalizedUserName = "superadmin@gmail.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //add all the roles to superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=adminRoleId,
                    UserId= superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId= superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId= superAdminId,
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }

}
