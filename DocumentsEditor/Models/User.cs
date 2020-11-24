using System.Collections.Generic;

namespace DocumentsEditor.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; } = 3.0;
        public ICollection<Document> Documents { get; set; }
    }
}
