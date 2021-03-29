using System;
using NLog.Web;
using System.IO;
using System.Linq;

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

                Console.WriteLine("1) Display all blogs.");
                Console.WriteLine("2) Add a blog.");
                Console.WriteLine("3) Create post.");
                Console.WriteLine("4) Display posts.");
                string input = Console.ReadLine();

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

                }

                //display posts
                if(input == "4")
                {

                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
