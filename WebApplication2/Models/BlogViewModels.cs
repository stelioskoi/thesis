using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class Post
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "ShortDescription")]

        [AllowHtml]
        public string ShortDescription { get; set; }
        [Required]

        [AllowHtml]
        [Display(Name = "Body")]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Meta")]
        public string Meta { get; set; }
        [Required]
        [Display(Name = "UrlSeo")]
        public string UrlSeo { get; set; }
        public bool Published { get; set; }
        [DefaultValue(0)]
        public int NetLikeCount { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }

  
        public ICollection<PostCategory> PostCategories { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<PostImage> PostImages { get; set; }
      
    }
    public class Category
        {
            public string id { get; set; }


            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "UrlSeo")]
            public string UrlSeo { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 5)]
            [Display(Name = "Description")]
            public string Description { get; set; }

            public bool Checked { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
    }

    public class PostCategory
        {   [Key]
        [Column(Order =0)]
        public string PostId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string CategoryId { get; set; }

        public bool Checked { get; set; }

        public Post Post { get; set; }

            public Category Category { get; set;} 
        }

    public class Tag
        {
            public string Id { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
            [Display(Name = "UrlSeo")]
            public string UrlSeo { get; set; }

            public bool Checked { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

        }

    public class PostTag
        {
            [Key]
            [Column(Order=0)]
            public string PostId { get; set; }
            [Key]
            [Column(Order = 1)]
            public string TagId { get; set; }
            public bool Checked { get; set; }

            public Post Post { get; set; }
            public Tag Tag { get; set; }

        }

    public class PostImage
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Imagename")]
            public string Imagename { get; set; }

        
             public string PostId { get; set; }
          
            public Post Post { get; set; }

            
    }


    //view model use
    public class BlogViewModel
    {
        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public IList<Tag> Tag { get; set; }

        public int TotalPosts { get; set; }

        public List<string> Category { get; set; }

        public Post Post { get; set; }

        public string ID { get; set; }

        public string ShortDescription { get; set; }

        public string Title { get; set; }

        public IList<Category> PostCategories { get; set; }

        public IList<Tag> PostTags { get; set; }

        public string UrlSlug { get; set; }

    }

    //model gia pinaka gia ola ta post 
    public class AllPostsViewModel
    {
        public IList<Category> PostCategories { get; set; }
        public IList<Tag> PostTags { get; set; }
        public string PostId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public bool Checked { get; set; }
        public Tag Tag { get; set; }
        public string UrlSlug { get; set; }
       

    }

    //model gia na emfanizei to view ksexorista tou kathe post
    public class PostViewModel
    {
        [AllowHtml]
        public string Body { get; set; }
        public string FirstPostId { get; set; }
        public string ID { get; set; }
        public string LastPostId { get; set; }
        public string NextPostSlug { get; set; }
        public int PostCount { get; set; }
       public string PreviousPostSlug { get; set; }
        public string Title { get; set; }
        public IList<PostImage> Images { get; set; }
        public IList<Tag> PostTags { get; set; }
        public string Meta { get; set; }
        public string UrlSeo { get; set; }
        public IList<Category> PostCategories { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Tag> Tags { get; set; }
        [NotMapped]
        public HttpPostedFileBase pic;


    }
}