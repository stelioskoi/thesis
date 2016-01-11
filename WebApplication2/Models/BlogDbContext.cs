using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BlogDbContext: DbContext
    {   //όνομα βάσης 
        public BlogDbContext() : base("Contacts")
        {
        }
        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
        //οι πινακες που θα δημιουργηθουν
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}