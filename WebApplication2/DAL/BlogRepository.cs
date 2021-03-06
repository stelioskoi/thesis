﻿using WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.DAL;

namespace WebApplication2
{
    public class BlogRepository : IBlogRepository, IDisposable
    {
        private BlogDbContext _context;
        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

       //get all posts
        public IList<Post> GetPosts()
        {
            
            return _context.Posts.ToList();
        }
        public IList<Tag> GetTags()
        {
            return _context.Tags.ToList();
        }
        public IList<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        //κατηγορίες
        public IList<Category> GetPostCategories(Post post)
        {
            //GetCategories category id of postcategory table
            var categoryIds = _context.PostCategories.Where(p => p.PostId == post.Id).Select(p => p.CategoryId).ToList();
            //creaty empty list of categories
            List<Category> categories = new List<Category>();
            //prosthiki ton id sti kaini lista
            foreach (var catId in categoryIds)
            {
                categories.Add(_context.Categories.Where(p => p.id == catId).FirstOrDefault());
            }
            return categories;
        }
        public IList<Tag> GetPostTags(Post post)
        {
            //idia diadikasia me ton getpostcategories
            var tagIds = _context.PostTags.Where(p => p.PostId == post.Id).Select(p => p.TagId).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (var tagId in tagIds)
            {
                tags.Add(_context.Tags.Where(p => p.Id == tagId).FirstOrDefault());
            }
            return tags;
        }
        public IList<PostImage> GetPostImages(Post post)
        {
            var postUrls = _context.PostImages.Where(p => p.PostId == post.Id).ToList();
            List<PostImage> images = new List<PostImage>();
            foreach (var url in postUrls)
            {
                images.Add(url);
            }
            return images;
        }
      
    

        public Post GetPostById(string id)
        {
            return _context.Posts.Find(id);
        }

        public string GetPostIdBySlug(string slug)
        {
            return _context.Posts.Where(x => x.UrlSeo == slug).FirstOrDefault().Id;
        }
        
        public void AddImageToPost(string postid, string Imagename)
        {
            List<int> numlist = new List<int>();
            int num = 1;
           
            var check = _context.PostImages.Where(x => x.PostId == postid && x.Imagename == Imagename).Any();
            
                var image = new PostImage { Id = num, PostId = postid, Imagename = Imagename };
                _context.PostImages.Add(image);
                Save();
        

        }
        public void RemoveImageFromPost(string postid, string picname)
        {
            var image = _context.PostImages.Where(x => x.PostId == postid && x.Imagename == picname).FirstOrDefault();
            _context.PostImages.Remove(image);
            Save();
        }


        public void AddPostCategories(PostCategory postCategory)
        {
            _context.PostCategories.Add(postCategory);
        }
        public void RemovePostCategories(string postid, string categoryid)
        {
            PostCategory postCategory = _context.PostCategories.Where(x => x.PostId == postid && x.CategoryId == categoryid).FirstOrDefault();
            _context.PostCategories.Remove(postCategory);
            Save();
        }
        public void RemoveCategoryFromPost(string postid, string catName)
        {
            var catid = _context.Categories.Where(x => x.Name == catName).Select(x => x.id).FirstOrDefault();
            var cat = _context.PostCategories.Where(x => x.PostId == postid && x.CategoryId == catid).FirstOrDefault();
            _context.PostCategories.Remove(cat);
            Save();
        }
        public void AddNewCategory(string catName, string catUrlSeo, string catDesc)
        {
            List<int> numlist = new List<int>();
            int num = 0;
            var categories = _context.Categories.ToList();
            foreach (var cat in categories)
            {
                var catid = cat.id;
                Int32.TryParse(catid.Replace("cat", ""), out num);
                numlist.Add(num);
            }
            numlist.Sort();
            num = numlist.Last();
            num++;
            var newid = "cat" + num.ToString();
            var category = new Category { id = newid, Name = catName, Description = catDesc, UrlSeo = catUrlSeo, Checked = false };
            _context.Categories.Add(category);
            Save();
        }


        public void RemoveTagFromPost(string postid, string tagName)
        {
            var tagid = _context.Tags.Where(x => x.Name == tagName).Select(x => x.Id).FirstOrDefault();
            var tag = _context.PostTags.Where(x => x.PostId == postid && x.TagId == tagid).FirstOrDefault();
            _context.PostTags.Remove(tag);
            Save();
        }
        public void AddPostTags(PostTag postTag)
        {
            _context.PostTags.Add(postTag);
        }
        public void RemovePostTags(string postid, string tagid)
        {
            PostTag postTag = _context.PostTags.Where(x => x.PostId == postid && x.TagId == tagid).FirstOrDefault();
            _context.PostTags.Remove(postTag);
            Save();
        }
        public void AddNewTag(string tagName, string tagUrlSeo)
        {
            List<int> numlist = new List<int>();
            int num = 0;
            var tags = _context.Tags.ToList();
            if (tags.Count() != 0)
            {
                foreach (var tg in tags)
                {
                    var tagid = tg.Id;
                    Int32.TryParse(tagid.Replace("tag", ""), out num);
                    numlist.Add(num);
                }
                numlist.Sort();
                num = numlist.Last();
                num++;
            }
            else
            {
                num = 1;
            }
            var newid = "tag" + num.ToString();
            var tag = new Tag { Id = newid, Name = tagName, UrlSeo = tagUrlSeo, Checked = false };
            _context.Tags.Add(tag);
            Save();
        }
        public void RemoveCategory(Category category)
        {
            var postCategories = _context.PostCategories.Where(x => x.CategoryId == category.id).ToList();
            foreach (var postCat in postCategories)
            {
                _context.PostCategories.Remove(postCat);
            }
            _context.Categories.Remove(category);
            Save();
        }
        public void RemoveTag(Tag tag)
        {
            var postTags = _context.PostTags.Where(x => x.TagId == tag.Id).ToList();
            foreach (var postTag in postTags)
            {
                _context.PostTags.Remove(postTag);
            }
            _context.Tags.Remove(tag);
            Save();
        }
        public void DeletePostandComponents(string postid)
        {
            var postCategories = _context.PostCategories.Where(p => p.PostId == postid).ToList();
             var postImages = _context.PostImages.Where(p => p.PostId == postid).ToList();
           var post = _context.Posts.Find(postid);
            foreach (var pc in postCategories) _context.PostCategories.Remove(pc);
           foreach (var pv in postImages) _context.PostImages.Remove(pv);
           
            
            _context.Posts.Remove(post);
            Save();
        }

        public void AddNewPost(Post post)
        {
            _context.Posts.Add(post);
            Save();
        }
        

      

        public void Save()
        {
            _context.SaveChanges();
        }


        #region dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion dispose
    }

}