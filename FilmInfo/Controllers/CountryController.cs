using FilmInfo.DAL;
using FilmInfo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmInfo.Controllers
{
    public class CountryController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Country/

        public ViewResult Index()
        {
            var countries = unitOfWork.CountryRepository.Get();
            return View(countries.ToList());
        }

        //
        // GET: /Country/Details/5

        public ViewResult Details(string id)
        {
            Country country = unitOfWork.CountryRepository.GetByID(id);
            return View(country);
        }

        //
        // GET: /country/Create

        public ActionResult Create()
        {
            PopulateCountriesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "id,Name,FilminCountry")]
         Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CountryRepository.Insert(country);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateCountriesDropDownList(country.Name);
            return View(country);
        }

        public ActionResult Edit(string id)
        {
            Country country = unitOfWork.CountryRepository.GetByID(id);
            PopulateCountriesDropDownList(country.Name);
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
             [Bind(Include = "id,Name,FilmInCountry")]
         Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CountryRepository.Update(country);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateCountriesDropDownList(country.Name);
            return View(country);
        }

        private void PopulateCountriesDropDownList(object selectedCountry = null)
        {
            var countriesQuery = unitOfWork.CountryRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.CountryID = new SelectList(countriesQuery, "ID", "Name", selectedCountry);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(string id)
        {
            Country country = unitOfWork.CountryRepository.GetByID(id);
            return View(country);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Country country = unitOfWork.CountryRepository.GetByID(id);
            unitOfWork.CountryRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}