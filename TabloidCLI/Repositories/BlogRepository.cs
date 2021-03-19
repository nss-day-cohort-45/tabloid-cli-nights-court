﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               Url
                                          FROM Blog";

                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),
                        };
                        blogs.Add(blog);
                    }

                    reader.Close();

                    return blogs;
                }
            }
        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Blog blog)
        {
            throw new NotImplementedException();
        }

        public void Update(Blog blog)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertTag(Blog blog, Tag tag)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(int blogId, int tagId)
        {
            throw new NotImplementedException();
        }
    }
}

