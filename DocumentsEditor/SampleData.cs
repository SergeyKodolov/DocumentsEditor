using DocumentsEditor.Models;
using System.Collections.Generic;
using System.Linq;

namespace DocumentsEditor
{
    public static class SampleData
    {
        public static void Initialize(DocumentsContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Login = "qwe",
                        Password = "qwe",
                        Documents = new List<Document> {
                            new Document
                            {
                                Name = "Документ1",
                                HtmlText = "My mother has <span style=\"font-size: 17px; font-weight: bold; font-style: italic\">blue</span> eyes."
                            },
                            new Document
                            {
                                Name = "Документ2",
                                HtmlText = "My mother has <span style=\"font-size: 17px; font-weight: bold; font-style: italic\">blue</span> eyes."
                            },
                            new Document
                            {
                                Name = "Документ3",
                                HtmlText = "My mother has <span style=\"font-size: 17px; font-weight: bold; font-style: italic\">blue</span> eyes."
                            }
                        }
                    },
                    new User
                    {
                        Login = "qweqwe",
                        Password = "qweqwe",
                        Documents = new List<Document> {
                            new Document
                            {
                                Name = "Документ1",
                                HtmlText = "<span style=\"font-size: 17px; font-weight: bold; font-style: italic\">blue</span>"
                            },
                            new Document
                            {
                                Name = "Документ2",
                                HtmlText = "My"
                            }
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
