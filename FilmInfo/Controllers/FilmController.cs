using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FilmInfo.Models;


namespace FilmInfo.Controllers
{


    public class FilmController : Controller
    {
        private FilmContext db = new FilmContext();

        // GET: /Film/

        public ActionResult Index(int page = 1)
        {
            int tpage = page - 1;
            var dataSource = db.Film;

            const int PageSize = 10;

            var count = dataSource.Count();

            IEnumerable<Film> OrderedDataSource = dataSource.OrderBy(film => film.Id);

            var data = OrderedDataSource.Skip(tpage * PageSize).Take(PageSize).ToList();

            this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            this.ViewBag.PageSize = PageSize;
            this.ViewBag.Count = count / PageSize;

            this.ViewBag.CurrentPage = page;
            return View(data);
        }

        // GET: /Film/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Film filminformation = db.Film.Find(id);
            FullFilmInfo fullinfo = new FullFilmInfo();

            string country = "", director = "";
            foreach (var item in filminformation.FilmInCountry)
            {
                country = country + item.Country.Name + " ";
            }
            foreach (var item in filminformation.FilmInDirector)
            {
                director = director + item.Director.Name + " ";
            }

            fullinfo.Id = filminformation.Id;
            fullinfo.OriginalName = filminformation.OriginalName;
            fullinfo.RussianName = filminformation.RussianName;
            fullinfo.PosterUrl = filminformation.PosterUrl;
            fullinfo.Year = filminformation.Year;
            fullinfo.Slogan = filminformation.Slogan;
            fullinfo.KPRatings = filminformation.KPRatings;
            fullinfo.IMDbRatings = filminformation.IMDbRatings;
            fullinfo.Description = filminformation.Description;
            fullinfo.Country = country;
            fullinfo.Director = director;

            if (fullinfo == null)
            {
                return HttpNotFound();
            }

            return View(fullinfo);
        }

        // GET: /Film/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Film/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OriginalName,RussianName,PosterUrl,Year,Slogan,KPRatings,IMDbRatings,Description")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Film.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: /Film/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: /Film/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OriginalName,RussianName,PosterUrl,Year,Slogan,KPRatings,IMDbRatings,Description")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: /Film/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: /Film/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Film film = db.Film.Find(id);
            db.Film.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
