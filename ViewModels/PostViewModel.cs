using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGBlog.ViewModels
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
        public string PostLink { get; set; }
        public string Slug { get; set; }
        public string SlugImageUrl { get; set; }
        public string Content { get; set; }
    }
}
