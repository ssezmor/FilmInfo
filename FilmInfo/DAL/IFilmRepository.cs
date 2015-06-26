using FilmInfo.Models;
using System;
using System.Collections.Generic;

namespace FilmInfo.DAL
{
    public class IFilmRepository : IDisposable
    {
        IEnumerable<Film> GetFilms();
        Film GetFilmByID(int filmId);
        void InsertFilm(Film film);
        void DeleteFilm(int filmID);
        void UpdateFilm(Film film);
        void Save();
    }
}