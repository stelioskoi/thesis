using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;
using System.Threading.Tasks;

namespace WebApplication2.DAL
{
    //idisposable Provides a mechanism for releasing unmanaged resources
    public interface IBlogRepository :IDisposable
    {

        IList<Post> GetPosts();

        IList<Category> GetPostCategories(Post post);

        IList<Tag> GetPostTags(Post post);

        IList<PostImage> GetPostImages(Post post);

        IList<Tag> GetTags();
        IList<Category> GetCategories();

        Post GetPostById(string postid);
        string GetPostIdBySlug(string slug);
       
        void AddImageToPost(string postid, string Imagename);
        void RemoveImageFromPost(string postid, string Imagename);
        void AddPostCategories(PostCategory postCategory);
        void RemovePostCategories(string postid, string categoryid);
        void RemoveCategoryFromPost(string postid, string catName);
        void AddNewCategory(string catName, string catUrlSeo, string catDesc);
        void RemoveTagFromPost(string postid, string tagName);
        void AddPostTags(PostTag postTag);
        void RemovePostTags(string postid, string tagid);
        void AddNewTag(string tagName, string tagUrlSeo);
        void RemoveCategory(Category category);
        void RemoveTag(Tag tag);
        void DeletePostandComponents(string postid);
        void AddNewPost(Post post);
        void Save();
    }
}