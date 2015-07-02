using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmInfo.Models;

namespace FilmInfo.DAL
{
    public class UnitOfWork : IDisposable
    {
        private FilmContext context = new FilmContext();
        private GenericRepository<Country> countryRepository;
        private GenericRepository<Director> directorRepository;

        public GenericRepository<Country> CountryRepository
        {
            get
            {

                if (this.countryRepository == null)
                {
                    this.countryRepository = new GenericRepository<Country>(context);
                }
                return countryRepository;
            }
        }

        public GenericRepository<Director> DirectorRepository
        {
            get
            {

                if (this.directorRepository == null)
                {
                    this.directorRepository = new GenericRepository<Director>(context);
                }
                return directorRepository;
            }
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