using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using System.Text;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid Selection");
                    Console.WriteLine();
                    return this;
            }
        }

        private void List()
        {
            Console.Clear();
            Console.WriteLine("------Blogs------");
            Console.WriteLine();
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"Title: {blog.Title} - URL: {blog.Url}");
                Console.WriteLine();
            }
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        private Blog Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Please choose an Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                Console.Clear();
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.Clear();
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.Write("Title: ");

            blog.Title = Console.ReadLine();

            Console.Write("URL: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);

            Console.Clear();
            Console.WriteLine($"{blog.Title} : {blog.Url} was successfully added!");
            Console.WriteLine();
        }

        private void Edit()
        {
            Blog blogToEdit = Choose("Which blog would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New blog title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }

            _blogRepository.Update(blogToEdit);

            Console.Clear();
            Console.WriteLine($"{blogToEdit.Title} : {blogToEdit.Url} was successfully edited.");
            Console.WriteLine();
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("Which blog would you like to remove?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
            Console.Clear();
            Console.WriteLine($"{blogToDelete.Title} : {blogToDelete.Url} was successfully removed.");
            Console.WriteLine();
        }
    }
}
