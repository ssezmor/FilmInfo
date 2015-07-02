using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FilmInfo.DAL;
using FilmInfo.Models;
using System;
using PagedList;
using System.Data;


namespace FilmInfo.Controllers
{


    public class FilmController : Controller
    {

        private IFilmRepository filmRepository;
        public FilmController()
        {
            this.filmRepository = new FilmRepository(new FilmContext());
        }

        public FilmController(IFilmRepository filmRepository)
        {
            this.filmRepository = filmRepository;
        }



        // GET: /Film/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int pageSize = 10;
            int Count = filmRepository.GetFilms().Count() / pageSize;
            ViewBag.Count = Count;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var films = from s in filmRepository.GetFilms()
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                films = films.Where(s => s.OriginalName.ToUpper().Contains(searchString.ToUpper())
                                       || s.RussianName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    films = films.OrderByDescending(s => s.OriginalName);
                    break;
                case "Date":
                    films = films.OrderBy(s => s.Year);
                    break;
                case "date_desc":
                    films = films.OrderByDescending(s => s.Year);
                    break;
                default:  // Name ascending 
                    films = films.OrderBy(s => s.OriginalName);
                    break;
            }



            int? CurrentPage = page;
            int pageNumber = (page ?? 1);

            this.ViewBag.CurrentPage = 1;


            return View(films.ToPagedList(pageNumber, pageSize));
        }




    
    //
      // GET: /Films/Details/5

      public ViewResult Details(int id)
      {
         Film film = filmRepository.GetFilmByID(id);
         return View(film);
      }

      //
      // GET: /film/Create

      public ActionResult Create()
      {
         return View();
      }

      //
      // POST: /film/Create

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(
         [Bind(Include = "OriginalName, RussianName, Year ")]
          Film film)
      {
         try
         {
            if (ModelState.IsValid)
            {
                filmRepository.InsertFilm(film);
                filmRepository.Save();
               return RedirectToAction("Index");
            }
         }
         catch ( DataException /* dex */)
         {
            //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
            ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
         }
         return View(film);
      }

      //
      // GET: /film/Edit/5

      public ActionResult Edit(int id)
      {
          Film film = filmRepository.GetFilmByID(id);
         return View(film);
      }

      //
      // POST: /Film/Edit/5

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(
         [Bind(Include = "OriginalName, RussianName, Year ")]
         Film film)
      {
         try
         {
            if (ModelState.IsValid)
            {
               filmRepository.UpdateFilm(film);
               filmRepository.Save();
               return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
            //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
            ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
         }
         return View(film);
      }

      //
      // GET: /film/Delete/5

      public ActionResult Delete(bool? saveChangesError = false, int id = 0)
      {
         if (saveChangesError.GetValueOrDefault())
         {
            ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
         }
         Film film = filmRepository.GetFilmByID(id);
         return View(film);
      }

      //
      // POST: /film/Delete/5

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(int id)
      {
         try
         {
             Film film = filmRepository.GetFilmByID(id);
             filmRepository.DeleteFilm(id);
             filmRepository.Save();
         }
         catch (DataException /* dex */)
         {
            //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
            return RedirectToAction("Delete", new { id = id, saveChangesError = true });
         }
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing)
      {
         filmRepository.Dispose();
         base.Dispose(disposing);
      }

}
}
