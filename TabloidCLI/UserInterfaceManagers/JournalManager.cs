using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        //Journal Menu
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Journal Entries");
            Console.WriteLine(" 3) Edit Journal Entries");
            Console.WriteLine(" 4) Remove Journal Entries");
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
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        //Display the list of Journal Entries
        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            Console.WriteLine("Journal Entries: ");
            Console.WriteLine("");
            foreach (Journal journal in journals)
            {
                Console.WriteLine($"  Title: {journal.Title} - Content: {journal.Content} - DateTime: {journal.CreateDateTime}");
            }
            Console.WriteLine("");
        }

        //Add a new Journal Entry
        private void Add()
        {
            List<Journal> journals = _journalRepository.GetAll();

            Journal journalEntry = new Journal();
            journalEntry.CreateDateTime = DateTime.Now;

            Console.Clear();
            Console.Write("Journal title: ");
            journalEntry.Title = Console.ReadLine();

            Console.Clear();
            Console.Write("Journal Content: ");
            journalEntry.Content = Console.ReadLine();
           
            _journalRepository.Insert(journalEntry);

            Console.Clear();
            Console.WriteLine($"\"{journalEntry.Title}\" was successfully added!");
            Console.WriteLine();
        }
        private void Edit()
        {
            Journal JournalEntryToEdit = Choose("Which journal entry would you like to edit?");
            if (JournalEntryToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New journal entry title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                JournalEntryToEdit.Title = title;
            };

            Console.Write("New Content (blank to leave unchanged): ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                JournalEntryToEdit.Content = content;
            };

           


            _journalRepository.Update(JournalEntryToEdit);

            Console.Clear();
            Console.WriteLine($"{JournalEntryToEdit.Title} : {JournalEntryToEdit.Content} was successfully edited.");
            Console.WriteLine();
        }

        //Choose a Journal Entry to do something with
        private Journal Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Please choose a Journal Entry :";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                Console.Clear();
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        //Delete a Journal Entry
        private void Remove()
        {
            Console.Clear();
            Journal JournalEntryToDelete = Choose("Which Journal Entry would you like to remove?");
            if (JournalEntryToDelete != null)
            {
                _journalRepository.Delete(JournalEntryToDelete.Id);
            }
            Console.Clear();
            Console.WriteLine($"{JournalEntryToDelete.Title} : {JournalEntryToDelete.CreateDateTime} was successfully removed.");
            Console.WriteLine();
        }
    }
    
}

