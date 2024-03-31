﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeBlog.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }

        public string? Heading { get; set; }

        public string? PageTitle { get; set; }

        public string? Content { get; set; }

        public string? ShortDescription { get; set; }

        public string? FeaturedImageUrl { get; set; }

        public string? UrlHandle { get; set; }

        public string? PublishedDate { get; set; }

        public string? Author { get; set; }

        public bool Visible { get; set; }

        //display the tag
        public IEnumerable<SelectListItem> Tags { get; set; }

        //collect tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
