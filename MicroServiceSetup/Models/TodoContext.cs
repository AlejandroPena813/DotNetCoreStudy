using Microsoft.EntityFrameworkCore;

namespace MicroServiceSetup.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> ToDoItems { get; set; } //todoitems is name of table in mysql, 'To do' is model
        
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) {} //default stuff. need to research setup properly
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            // call base 
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Todo>() 
                .ToTable("ToDoItems"); // custom
            
            modelBuilder.Entity<Todo>()
                .Property(p => p.IsComplete)
                .HasColumnType("bit"); //mysql stores bit, c# type is bool
        }
    }
    
}