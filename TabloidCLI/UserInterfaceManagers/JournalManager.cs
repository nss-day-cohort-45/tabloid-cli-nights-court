using System;
using System.Collections.Generic;
using System.Text;
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

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal");
            Console.WriteLine(" 2) Add Journal");
            Console.WriteLine(" 3) Edit Journal");
            Console.WriteLine(" 4) Remove Journal");
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

        }
        private void Remove()
        {

        }
    }
    
}

