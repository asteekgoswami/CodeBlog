using Microsoft.AspNetCore.Identity;

namespace CodeBlog.Repositories.Interface
{
	public interface IUserRespository
	{
		Task<IEnumerable<IdentityUser>> GetAll();
	}
}
