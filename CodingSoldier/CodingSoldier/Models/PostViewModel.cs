using CodingSoldier.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CodingSoldier.Models
{
    public class PostViewModel : BaseViewModel
    {
        //public PostViewModel()
        //{

        //}

        //public PostViewModel(Post post)            
        //{
        //    PostId = post.PostId;
        //    PostTitle = post.PostTitle;
        //    PostContent = post.PostContent;
        //    IsAQuestion = post.IsAQuestion;
        //    Tags = post.Tags;
        //    CategoryName = post.CategoryName;
        //}

        public int Id { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        public bool IsAQuestion { get; set; }
        [Required]
        public string Tags { get; set; }
        [Required]
        public string CategoryName { get; set; }

        //public Post ConvertToModel()
        //{
        //    return new Post
        //    {
        //        PostId = PostId,
        //        PostTitle = PostTitle,
        //        PostContent = PostContent,
        //        IsAQuestion = IsAQuestion,
        //        Tags = Tags,
        //        CategoryName = CategoryName
        //    };
        //}
    }
}