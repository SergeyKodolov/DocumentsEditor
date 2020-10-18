using DocumentsEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocumentsEditor
{
    public static class SampleData
    {
        public static void Initialize(DocumentsContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(
                    new User
                    {
                        Login = "qwe",
                        Password = "qwe",
                        Documents = new List<Document> {
                            new Document
                            {
                                Name = "Документ1",
                                HtmlText = "My mother has <span style=\"font-size: 17px; font-weight: bold; font-style: italic\">blue</span> eyes."
                            }
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
