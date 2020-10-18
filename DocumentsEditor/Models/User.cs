using System.Collections.Generic;

namespace DocumentsEditor.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Document> Documents { get; set; }
    }
}
