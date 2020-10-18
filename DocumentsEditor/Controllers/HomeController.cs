using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DocumentsEditor.Models;
using Microsoft.AspNetCore.Authorization;

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
            var user = User.Identity.Name;
            return View(db.Documents.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateDocument()
        {
            var doc = new Document { Name = "Новый документ" };
            db.Documents.Add(doc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditDocument(int? id)
        {
            if (id != null)
            {
                Document doc = db.Documents.FirstOrDefault(p => p.Id == id);
                if (doc != null)
                    ViewBag.DocumentId = id;
                    return View(doc);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditDocument(Document doc)
        {
            db.Documents.Update(doc);
            db.SaveChanges();
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
                    db.Documents.Remove(doc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
