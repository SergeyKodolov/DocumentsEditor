using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DocumentsEditor.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HtmlText { get; set; }
    }
}
