using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System;


namespace Library.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _db;

        public AuthorsController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Author> model = _db.Authors.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisAuthor = _db.Authors
                .Include(author => author.Books)
                .ThenInclude(join => join.Book)
                .FirstOrDefault(author => author.AuthorId == id);
            return View(thisAuthor);
        }

        [HttpPost]
        public ActionResult Index (string search)
        {
            List<Author> model = _db.Authors.ToList();
            if(!String.IsNullOrEmpty(search))
           {
               model = model.Where(author => author.AuthorName.ToLower().Contains(search.ToLower())).Select(author => author).ToList();
           }
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
            return View(thisAuthor);
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            _db.Entry(author).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult AddBook(int id)
        {
            var thisAuthor = _db.Authors.FirstOrDefault(authors => authors.AuthorId == id);
            ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookTitle");
            return View(thisAuthor);
        }
        [HttpPost]
        public ActionResult AddBook(Author author, int BookId)
        {
            if (BookId != 0)
            {
                _db.AuthorBooks.Add(new AuthorBook() { BookId = BookId, AuthorId = author.AuthorId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
            return View(thisAuthor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
            _db.Authors.Remove(thisAuthor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}