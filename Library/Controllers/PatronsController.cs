using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        private readonly LibraryContext _db;

        public PatronsController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Patrons.ToList());
        }
        public ActionResult Details(int id)
        {
            var thisPatron = _db.Patrons
            .Include(patron => patron.Rentals)
            .ThenInclude(join => join.Copy)
            .FirstOrDefault(patron => patron.PatronId == id);
            return View(thisPatron);
        }
        
        public ActionResult Create()
        {
            ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patron patron, int CopyId)
        {
            _db.Patrons.Add(patron);
            if (CopyId != 0)
            {
                _db.Checkouts.Add(new Checkout() { CopyId = CopyId, PatronId = patron.PatronId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyAmnt");
            return View(thisPatron);
        }

        [HttpPost]
        public ActionResult Edit(Patron patron, int CopyId)
        {
            if (CopyId != 0)
            {
                _db.Checkouts.Add(new Checkout() { CopyId = CopyId, PatronId = patron.PatronId });
            }
            _db.Entry(patron).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddCopy(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyAmnt");
            return View(thisPatron);
        }
        [HttpPost]
        public ActionResult AddCopy(Patron patron, int CopyId)
        {
            if (CopyId != 0)
            {
                _db.Checkouts.Add(new Checkout() { CopyId = CopyId, PatronId = patron.PatronId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            return View(thisPatron);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
            _db.Patrons.Remove(thisPatron);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteCopy(int joinId)
        {
            var joinEntry = _db.Checkouts.FirstOrDefault(entry => entry.CheckoutId == joinId);
            _db.Checkouts.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}