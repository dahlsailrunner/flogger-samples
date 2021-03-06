﻿using Flogging.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace mvc_todo.Models
{

    public class ToDoItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public bool Completed { get; set; }
    }

    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext()
        {
            DbInterception.Add(new FloggerEFInterceptor());
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }

}