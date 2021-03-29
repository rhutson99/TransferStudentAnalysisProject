using System;
using NLog.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

 try
            {
                var db = new BloggingContext();
                string input;


                do{
                Console.WriteLine("1) Display all blogs.");
                Console.WriteLine("2) Add a blog.");
                Console.WriteLine("3) Create post.");
                Console.WriteLine("4) Display posts.");
                Console.WriteLine("press any other key to quit.");
                input = Console.ReadLine();

                //display all blogs
                if(input == "1")
                {
                var query = db.Blogs.OrderBy(b => b.Name);

                int count = db.Blogs.Count();
                Console.WriteLine($"{count} blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                }

                //create new blog
                if(input == "2")
                {
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                
                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);
                }

                //create new post
                if(input == "3")
                {
                var query = db.Blogs.OrderBy(b => b.Name);
                foreach (var item in query)
                {
                    Console.WriteLine($"ID: {item.BlogId}) Name: {item.Name}");
                }


                    Console.WriteLine("Enter name of the blog to post to");
                    
                    string Pto = Console.ReadLine();


                    var exists = db.Blogs.Any(b => b.Name.Contains($"{Pto}"));

                    if(exists == true)
                    {

                    var post = new Post();
                    var BlogSearch = db.Blogs.Where(b => b.Name.Contains($"{Pto}"));
                    List<Blog> blog = new List<Blog>(BlogSearch);
                    var IdSearch = db.Blogs.Where(b => b.Name.Contains($"{Pto}")).Select(b => b.BlogId);
                    List<int> Id = new List<int>(IdSearch);

                    post.Blog = blog[0];
                    post.BlogId = Id[0];
                    
                    Console.WriteLine("Please enter a title.");
                    post.Title = Console.ReadLine();

                    Console.WriteLine("Please enter the post content.");
                    post.Content = Console.ReadLine();

                    db.AddPost(post);
                    logger.Info("Post Added");
                    }

                    else
                    {
                        Console.WriteLine("Blog not found");
                    }


                }

                //display posts
                if(input == "4")
                {

                var query = db.Blogs.OrderBy(b => b.Name);
                foreach (var item in query)
                {
                    Console.WriteLine($"ID: {item.BlogId}) Name: {item.Name}");
                }


                    Console.WriteLine("Enter name of the blog to display posts from");
                    
                    string display = Console.ReadLine();


                    var exists = db.Blogs.Any(b => b.Name.Contains($"{display}"));

                    if(exists == true)
                    {

                    var IdSearch = db.Blogs.Where(b => b.Name.Contains($"{display}")).Select(b => b.BlogId);
                    List<int> Id = new List<int>(IdSearch);
                    int BlogId = Id[0];

                int count = db.Posts.Where(b => b.BlogId.Equals(Id[0])).Count();
                Console.WriteLine($"{count} Posts in this Blog.");

                        var Dposts = db.Posts.Where(b => b.BlogId.Equals(Id[0]));
                        foreach (var item in Dposts)
                        {
                            Console.WriteLine($"Blog: {display} \nTitle: {item.Title} \nContent: {item.Content}");
                        }

                    }

                    else
                    {
                        Console.WriteLine("Blog not found");
                    }


                }
                }while(input == "1" || input == "2" || input == "3" || input == "4");


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
