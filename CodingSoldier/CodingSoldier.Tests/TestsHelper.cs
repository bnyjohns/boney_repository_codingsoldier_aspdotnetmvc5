using AutoMapper;
using CodingSoldier.Core.Entities;
using CodingSoldier.Core.Models;
using CodingSoldier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CodingSoldier.Tests
{
    public class TestsHelper
    {
        internal static IEnumerable<Category> GetInMemoryCagegories()
        {
            return new List<Category>
            {
                new Category { CategoryName = "C#.NET" },
                new Category { CategoryName = "JavaScript" },
                new Category { CategoryName = "ASP.NET" },
                new Category { CategoryName = "SQL" },
                new Category { CategoryName = "MVC" }
            };
        }

        internal static List<SelectListItem> GetCategories()
        {
            return GetInMemoryCagegories().
                Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryName
                }).ToList();
        }

        internal static IEnumerable<Post> GetInMemoryPosts()
        {
            return new List<Post>()
            {
                new Post { Id = 1, PostTitle = "First Post Title", PostContent = "First Post Content", CategoryName = "C#.NET" },
                new Post { Id = 2, PostTitle = "Second Post Title", PostContent = "Second Post Content", CategoryName = "ASP.NET" },
                new Post { Id = 3, PostTitle = "Third Post Title", PostContent = "Third Post Content", CategoryName = "SQL" },
                new Post { Id = 4, PostTitle = "Fourth Post Title", PostContent = "Fourth Post Content", CategoryName = "MVC" },
                new Post { Id = 5, PostTitle = "Fifth Post Title", PostContent = "Fifth Post Content", CategoryName = "JavaScript" },
                new Post { Id = 6, PostTitle = "Sixth Post Title", PostContent = "Sixth Post Content", CategoryName = "JavaScript" },
            };
        }

        internal static void InitializeAutoMapper()
        {
            Mapper.Initialize(mapperConfiguration =>
            {
                mapperConfiguration.CreateMap<Post, PostViewModel>().ReverseMap();
                mapperConfiguration.CreateMap<Post, PostApiEntity>().ReverseMap();
            });
        }
    }
}
