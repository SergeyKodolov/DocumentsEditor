using DocumentsEditor.Models;
using System.Collections.Generic;

namespace DocumentsEditor.ViewModels
{
    public class HomeModel
    {
        public IEnumerable<Document> Documents { get; set; }
        public User CurrentUser { get; set; }
    }
}
