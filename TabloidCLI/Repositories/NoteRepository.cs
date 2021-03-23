using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Note note)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(Note note)
        {
            throw new NotImplementedException();
        }
    }
}