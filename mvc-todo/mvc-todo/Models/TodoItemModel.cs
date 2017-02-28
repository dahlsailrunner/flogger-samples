using System.Data.Entity;

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
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }

}