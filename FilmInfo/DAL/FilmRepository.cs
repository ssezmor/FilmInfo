using FilmInfo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilmInfo.DAL
{
    public class FilmRepository : IFilmRepository, IDisposable
    {
        private FilmContext context;
        public FilmRepository(FilmContext context)
        {
            this.context = context;
        }

        public IEnumerable<Film> GetFilms()
        {
            return context.Film.ToList();
        }

        public Film GetFilmByID(int id)
        {
            return context.Film.Find(id);
        }

        public void InsertFilm(Film film)
        {
            context.Film.Add(film);
        }

        public void DeleteFilm(int filmID)
        {
            Film film = context.Film.Find(filmID);
            context.Film.Remove(film);
        }

        public void UpdateFilm(Film film)
        {
            context.Entry(film).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }






    }



}