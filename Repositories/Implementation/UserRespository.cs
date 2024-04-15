using CodeBlog.Data;
using CodeBlog.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.Repositories.Implementation
{
	public class UserRespository : IUserRespository
	{
		private readonly CodeBlogDbContext dbContext;

		public UserRespository(CodeBlogDbContext dbContext)
        {
			this.dbContext = dbContext;
		}
        public async  Task<IEnumerable<IdentityUser>> GetAll()
		{
			var users = await dbContext.Users.ToListAsync();
			var superAdminUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@gmail.com");

			if(superAdminUser != null)
			{
				users.Remove(superAdminUser);
			}
			return users;
		}
	}
}
