using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DocumentsEditor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace DocumentsEditor.Controllers
{
    public class HomeController : Controller
    {
        DocumentsContext db;

        public HomeController(DocumentsContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var result = db.Documents.Where(d => d.User.Login == User.Identity.Name);            
            return View(result.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateDocument()
        {
            var user = db.Users.Where(u => u.Login == User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var doc = new Document { Name = "Новый документ", User = user };
                db.Documents.Add(doc);
                db.SaveChanges();
            }            
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditDocument(int? id)
        {
            if (id != null)
            {
                Document doc = db.Documents.FirstOrDefault(d => d.Id == id);                

                if (doc != null)
                {
                    var login = db.Users.FirstOrDefault(u => u.Id == doc.UserId).Login;
                    if (login == User.Identity.Name)
                    {
                        return View(doc);
                    }
                    else
                    {
                        return Forbid();
                    }
                }       
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditDocument(Document doc)
        {
            var user = db.Users.Where(u => u.Login == User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                db.Documents.Update(doc);
                db.SaveChanges();
            }            
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteDocument(int? id)
        {
            if (id != null)
            {
                Document doc = db.Documents.FirstOrDefault(p => p.Id == id);
                if (doc != null)
                {
                    var login = db.Users.FirstOrDefault(u => u.Id == doc.UserId).Login;
                    if (login == User.Identity.Name)
                    {
                        db.Documents.Remove(doc);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Forbid();
                    }
                }
            }
            return NotFound();
        }
    }
}
