﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.DAL;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;

        public static List<BlogViewModel> postList = new List<BlogViewModel>();
        public static List<AllPostsViewModel> allPostsList = new List<AllPostsViewModel>();
        public static List<AllPostsViewModel> checkCatList= new List<AllPostsViewModel>();
        public static List<AllPostsViewModel> checkTagList = new List<AllPostsViewModel>();

        public BlogController()
        {
            _blogRepository = new BlogRepository(new BlogDbContext());
        }

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public ActionResult Index(int? page, string sortOrder, string searchString, string[] searchCategory, string[] searchTag)
        {

            checkCatList.Clear();
            checkTagList.Clear();
            CreateCatAndTagList();
            Posts(page, sortOrder, searchString, searchCategory, searchTag);
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddNewPost()
        {
            List<int> numlist = new List<int>();
            int num = 0;
            var posts = _blogRepository.GetPosts();
            if (posts.Count != 0)
            {
                foreach (var post in posts)
                {
                    var postid = post.Id;
                    Int32.TryParse(postid, out num);
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
            var newid = num.ToString();
            PostViewModel model = new PostViewModel();
            model.ID = newid;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddNewPost(PostViewModel model)
        {
            var post = new Post
            {
                Id = model.ID,
                Body = model.Body,
                Meta = model.Meta,
                PostedOn = DateTime.Now,
                Published = true,
                ShortDescription = model.ShortDescription,
                Title = model.Title,
                UrlSeo = model.UrlSeo
            };
            _blogRepository.AddNewPost(post);
            return RedirectToAction("AllPosts", "Blog", new { slug = model.UrlSeo });
        }
        public ActionResult AllPosts(int? page, string sortOrder, string searchString, string[] searchCategory, string[] searchTag)
        {
            allPostsList.Clear();
            checkCatList.Clear();
            checkTagList.Clear();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentSearchCategory = searchCategory;
            ViewBag.CurrentSearchTag = searchTag;
            ViewBag.DateSortParm = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            var posts = _blogRepository.GetPosts();
            foreach (var post in posts)
            {
                var postCategories = GetPostCategories(post);
                var postTags = GetPostTags(post);
                allPostsList.Add(new AllPostsViewModel() { PostId = post.Id, Date = post.PostedOn, Description = post.ShortDescription, Title = post.Title, PostCategories = postCategories, PostTags = postTags, UrlSlug = post.UrlSeo });
            }

            if (searchString != null)
            {
                allPostsList = allPostsList.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }

            CreateCatAndTagList();

            if (searchCategory != null)
            {
                List<AllPostsViewModel> newlist = new List<AllPostsViewModel>();
                foreach (var catName in searchCategory)
                {
                    foreach (var item in allPostsList)
                    {
                        if (item.PostCategories.Where(x => x.Name == catName).Any())
                        {
                            newlist.Add(item);
                        }
                    }
                    foreach (var item in checkCatList)
                    {
                        if (item.Category.Name == catName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                allPostsList = allPostsList.Intersect(newlist).ToList();
            }

            if (searchTag != null)
            {
                List<AllPostsViewModel> newlist = new List<AllPostsViewModel>();
                foreach (var tagName in searchTag)
                {
                    foreach (var item in allPostsList)
                    {
                        if (item.PostTags.Where(x => x.Name == tagName).Any())
                        {
                            newlist.Add(item);
                        }
                    }
                    foreach (var item in checkTagList)
                    {
                        if (item.Tag.Name == tagName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                allPostsList = allPostsList.Intersect(newlist).ToList();
            }

            switch (sortOrder)
            {
                case "date_desc":
                    allPostsList = allPostsList.OrderByDescending(x => x.Date).ToList();
                    break;
                case "Title":
                    allPostsList = allPostsList.OrderBy(x => x.Title).ToList();
                    break;
                case "title_desc":
                    allPostsList = allPostsList.OrderByDescending(x => x.Title).ToList();
                    break;
                default:
                    allPostsList = allPostsList.OrderBy(x => x.Date).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View("AllPosts", allPostsList.ToPagedList(pageNumber, pageSize));

        }

        [ChildActionOnly]
        public ActionResult Posts(int? page, string sortOrder, string searchString, string[] searchCategory, string[] searchTag)
        {
            postList.Clear();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentSearchCategory = searchCategory;
            ViewBag.CurrentSearchTag = searchTag;
            ViewBag.DateSortParm = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            var posts = _blogRepository.GetPosts();
            foreach (var post in posts)
            {
                var postCategories = GetPostCategories(post);
                var postTags = GetPostTags(post);
                postList.Add(new BlogViewModel() { Post = post, Modified = post.Modified, Title = post.Title, ShortDescription = post.ShortDescription, PostedOn = post.PostedOn, ID = post.Id, PostCategories = postCategories, PostTags = postTags, UrlSlug = post.UrlSeo });
            }



            if (searchString != null)
            {
                postList = postList.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (searchCategory != null)
            {
                List<BlogViewModel> newlist = new List<BlogViewModel>();
                foreach (var catName in searchCategory)
                {
                    foreach (var item in postList)
                    {
                        if (item.PostCategories.Where(x => x.Name == catName).Any())
                        {
                            newlist.Add(item);
                        }
                    }
                    foreach (var item in checkCatList)
                    {
                        if (item.Category.Name == catName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                postList = postList.Intersect(newlist).ToList();
            }

            if (searchTag != null)
            {
                List<BlogViewModel> newlist = new List<BlogViewModel>();
                foreach (var tagName in searchTag)
                {
                    foreach (var item in postList)
                    {
                        if (item.PostTags.Where(x => x.Name == tagName).Any())
                        {
                            newlist.Add(item);
                        }
                    }
                    foreach (var item in checkTagList)
                    {
                        if (item.Tag.Name == tagName)
                        {
                            item.Checked = true;
                        }
                    }
                }
                postList = postList.Intersect(newlist).ToList();
            }
            switch (sortOrder)
            {
                case "date_desc":
                    postList = postList.OrderByDescending(x => x.PostedOn).ToList();
                    break;
                case "Title":
                    postList = postList.OrderBy(x => x.Title).ToList();
                    break;
                case "title_desc":
                    postList = postList.OrderByDescending(x => x.Title).ToList();
                    break;
                default:
                    postList = postList.OrderBy(x => x.PostedOn).ToList();
                    break;
            }


            int pageSize = 2;
            int pageNumber = (page ?? 1);

            return PartialView("Posts", postList.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Post(string sortOrder, string slug)
        {
            PostViewModel model = new PostViewModel();
            var posts = GetPosts();
            var postid = _blogRepository.GetPostIdBySlug(slug);
            var post = _blogRepository.GetPostById(postid);
            var videos = GetPostVideos(post);
            var firstPostId = posts.OrderBy(i => i.PostedOn).First().Id;
            var lastPostId = posts.OrderBy(i => i.PostedOn).Last().Id;
            var nextId = posts.OrderBy(i => i.PostedOn).SkipWhile(i => i.Id != postid).Skip(1).Select(i => i.Id).FirstOrDefault();
            var previousId = posts.OrderBy(i => i.PostedOn).TakeWhile(i => i.Id != postid).Select(i => i.Id).LastOrDefault();
            model.FirstPostId = firstPostId;
            model.LastPostId = lastPostId;
            model.PreviousPostSlug = posts.Where(x => x.Id == previousId).Select(x => x.UrlSeo).FirstOrDefault();
            model.NextPostSlug = posts.Where(x => x.Id == nextId).Select(x => x.UrlSeo).FirstOrDefault();
            model.ID = post.Id;
            model.PostCount = posts.Count();
            model.UrlSeo = post.UrlSeo;
            model.Videos = videos;
            model.Title = post.Title;
            model.Body = post.Body;
            
            return View(model);
        }


        public IList<Post> GetPosts()
        {
            return _blogRepository.GetPosts();
        }

        public IList<Category> GetPostCategories(Post post)
        {
            return _blogRepository.GetPostCategories(post);
        }

        public IList<Tag> GetPostTags(Post post)
        {
            return _blogRepository.GetPostTags(post);
        }
        public IList<PostVideo> GetPostVideos(Post post)
        {
            return _blogRepository.GetPostVideos(post);
        }
        public void CreateCatAndTagList()
        {
            foreach (var ct in _blogRepository.GetCategories())
            {
                checkCatList.Add(new AllPostsViewModel { Category = ct, Checked = false });
            }
            foreach (var tg in _blogRepository.GetTags())
            {
                checkTagList.Add(new AllPostsViewModel { Tag = tg, Checked = false });
            }
        }
    }
}