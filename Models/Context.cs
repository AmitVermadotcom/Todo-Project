using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace todoonboard_api.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<User> Users {get; set;} = null!;
    }
}