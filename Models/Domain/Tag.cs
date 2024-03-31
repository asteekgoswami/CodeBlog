namespace CodeBlog.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        //for many to many relationship
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
