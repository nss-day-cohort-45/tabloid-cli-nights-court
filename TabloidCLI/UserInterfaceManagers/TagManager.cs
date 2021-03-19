﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            Console.Clear();
            Console.WriteLine("------Tags------");
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine(tag);
            }
            Console.WriteLine("----------------");
            Console.WriteLine();
        }
        private void Add()
        {
            Console.Clear();
            Console.WriteLine("New Tag");
            Tag tag = new Tag();

            Console.Write("Tag Name: ");
            tag.Name = Console.ReadLine();

            _tagRepository.Insert(tag);

            Console.Clear();
            Console.WriteLine($"The ({tag.Name}) tag was successfully added!");
            Console.WriteLine();
        }

        private Tag Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Please choose a Tag:";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                Console.Clear();
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Console.Clear();
            Tag tagToEdit = Choose("Which tag would you like to edit?");
            if (tagToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New tag name (blank to leave unchanged): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                tagToEdit.Name = name;
            }

            _tagRepository.Update(tagToEdit);
            Console.Clear();
            Console.WriteLine($"The ({tagToEdit.Name}) tag was successfully edited.");
            Console.WriteLine();
        }

        private void Remove()
        {
            Console.Clear();
            Tag tagToDelete = Choose("Which tag would you like to remove?");
            if (tagToDelete != null)
            {
                _tagRepository.Delete(tagToDelete.Id);
            }
            Console.Clear();
            Console.WriteLine($"{tagToDelete.Name} was successfully removed.");
            Console.WriteLine();
        }
    }
}
