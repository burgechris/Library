using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class CopiesController : Controller
    {
        private readonly LibraryContext _db;

        public CopiesController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Copy> model = _db.Copies.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Copy copy)
        {
            _db.Copies.Add(copy);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisCopy = _db.Copies
                .Include(copy => copy.Patrons)
                .ThenInclude(join => join.Patron)
                .FirstOrDefault(copy => copy.CopyId == id);
            return View(thisCopy);
        }

        public ActionResult Edit(int id)
        {
            var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
            return View(thisCopy);
        }

        [HttpPost]
        public ActionResult Edit(Copy copy)
        {
            _db.Entry(copy).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
            return View(thisCopy);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
            _db.Copies.Remove(thisCopy);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}