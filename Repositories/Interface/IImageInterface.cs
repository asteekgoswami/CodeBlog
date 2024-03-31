namespace CodeBlog.Repositories.Interface
{
    public interface IImageInterface
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
