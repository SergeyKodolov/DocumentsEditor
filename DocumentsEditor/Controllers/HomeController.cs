using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DocumentsEditor.Models;
using System.Data.Common;

namespace DocumentsEditor.Controllers
{
    public class HomeController : Controller
    {
        DocumentsContext db;

        public HomeController(DocumentsContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        [HttpGet]
        public IActionResult EditDocument(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.DocumentId = id;
            return View();
        }

        [HttpPost]
        public string EditDocument(Document doc)
        {
            db.Documents.Update(doc);
            db.SaveChanges();
            return "Изменения сохранены!";
        }
    }
}
