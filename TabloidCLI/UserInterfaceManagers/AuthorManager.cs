﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class AuthorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private string _connectionString;

        public AuthorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Author Menu");
            Console.WriteLine(" 1) List Authors");
            Console.WriteLine(" 2) Author Details");
            Console.WriteLine(" 3) Add Author");
            Console.WriteLine(" 4) Edit Author");
            Console.WriteLine(" 5) Remove Author");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Author author = Choose();
                    if (author == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new AuthorDetailManager(this, _connectionString, author.Id);
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
            Console.WriteLine("------Authors------");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine(author);
            }
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        private Author Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                Console.Clear();
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.Clear();
            Console.WriteLine("New Author");
            Author author = new Author();

            Console.Write("First Name: ");
            author.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            author.LastName = Console.ReadLine();

            Console.Write("Bio: ");
            author.Bio = Console.ReadLine();

            _authorRepository.Insert(author);

            Console.Clear();
            Console.WriteLine($"{author.FirstName} {author.LastName} was successfully added!");
            Console.WriteLine();
        }

        private void Edit()
        {
            Console.Clear();
            Author authorToEdit = Choose("Which author would you like to edit?");
            if (authorToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New first name (blank to leave unchanged): ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                authorToEdit.FirstName = firstName;
            }
            Console.Write("New last name (blank to leave unchanged): ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                authorToEdit.LastName = lastName;
            }
            Console.Write("New bio (blank to leave unchanged): ");
            string bio = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(bio))
            {
                authorToEdit.Bio = bio;
            }

            _authorRepository.Update(authorToEdit);
            Console.Clear();
            Console.WriteLine($"{authorToEdit.FirstName} {authorToEdit.LastName} was successfully edited.");
            Console.WriteLine();
        }

        private void Remove()
        {
            Console.Clear();
            Author authorToDelete = Choose("Which author would you like to remove?");
            if (authorToDelete != null)
            {
                _authorRepository.Delete(authorToDelete.Id);
                Console.Clear();
                Console.WriteLine($"{authorToDelete.FirstName} {authorToDelete.LastName} was successfully removed.");
            }
            Console.WriteLine();
        }
    }
}
