using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DocumentsEditor.Models;
using DocumentsEditor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace DocumentsEditor.Controllers
{
    public class HomeController : Controller
    {
        DocumentsContext db;
        User user;
        List<Document> documents;

        public HomeController(DocumentsContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            user = db.Users.FirstOrDefault(d => d.Login == User.Identity.Name);
            documents = db.Documents.Where(d => d.User.Login == User.Identity.Name).ToList();

            HomeModel homeView = new HomeModel { CurrentUser = user, Documents = documents };
            return View(homeView);
        }

        [Authorize]
        [HttpGet]
        public IActionResult TopUpBalance()
        {
            var user = db.Users.FirstOrDefault(u => u.Login == User.Identity.Name);

            if (user != null)
            {
                return View(user);
            }
            
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public IActionResult TopUpBalance(User u)
        {
            var user = db.Users.FirstOrDefault(user => user.Id == u.Id);

            user.Balance += u.Balance;

            db.Users.Update(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateDocument()
        {
            var user = db.Users.Where(u => u.Login == User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                if (user.Balance >= 0)
                {
                    user.Balance--;
                    var doc = new Document { Name = "Новый документ", User = user };
                    db.Users.Update(user);
                    db.Documents.Add(doc);
                    db.SaveChanges();
                }                             
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
                    var user = db.Users.FirstOrDefault(u => u.Id == doc.UserId);
                    if (user.Login == User.Identity.Name)
                    {
                        user.Balance++;
                        db.Users.Update(user);
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
