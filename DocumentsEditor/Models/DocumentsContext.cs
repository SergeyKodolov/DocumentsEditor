using Microsoft.EntityFrameworkCore;

namespace DocumentsEditor.Models
{
    public class DocumentsContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }

        public DocumentsContext(DbContextOptions<DocumentsContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
