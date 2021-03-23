using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private string _connectionString;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
            _postRepository = new PostRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Note");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Go Back");

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
            Console.WriteLine("------Notes------");
            Console.WriteLine();
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note note in notes)
            {
                // COME BACK TO ADD POST TITLE AS WELL
                Console.WriteLine($"Title: {note.Title} - Content: {note.Content} - CreateDateTime: {note.CreateDateTime}");
                Console.WriteLine();
            }
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        private Note Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Please choose an Note:";
            }

            Console.WriteLine(prompt);

            List<Note> notes = _noteRepository.GetAll();

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($" {i + 1}) {note.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                Console.Clear();
                int choice = int.Parse(input);
                return notes[choice - 1];
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
            Console.WriteLine("New Note");
            Note note = new Note();

            Post PostToAdd  = ChoosePost("Which post would you like to make a note on?");
            if (PostToAdd == null)
            {
                Console.WriteLine("post to add was null");
                return;
            }
            note.PostId = PostToAdd.Id;


            Console.Write("Title: ");
            note.Title = Console.ReadLine();

            Console.Write("Content: ");
            note.Content = Console.ReadLine();

          
           // _postRepository.GetAll();

            _noteRepository.Insert(note);

            Console.Clear();
            Console.WriteLine($"{note.Title} was successfully added!");
            Console.WriteLine();
        }

        private void Remove()
        {
            Note noteToDelete = Choose("Which note would you like to remove?");
            if (noteToDelete != null)
            {
                _noteRepository.Delete(noteToDelete.Id);
            }
            Console.Clear();
            Console.WriteLine($"{noteToDelete.Title} was successfully removed.");
            Console.WriteLine();
        }
        private Post ChoosePost(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                Console.Clear();
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception )
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
    }
}
