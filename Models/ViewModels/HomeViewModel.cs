﻿using CodeBlog.Models.Domain;

namespace CodeBlog.Models.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
