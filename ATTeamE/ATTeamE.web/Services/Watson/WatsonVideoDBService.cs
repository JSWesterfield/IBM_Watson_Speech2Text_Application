using ATTeamE.web.Domain.DB;
using ATTeamE.web.Interfaces.Services;
using ATTeamE.web.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WikiDataProvider.Data;

namespace ATTeamE.web.Services.Watson
{
    public class WatsonVideoDBService : BaseService, IDBService<Movie, int, object, object>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(object model)
        {
            throw new NotImplementedException();
        }

        public void Update(object model)
        {
            throw new NotImplementedException();
        }

        public Movie Get(int id)
        {
            Movie movie = null;
            DataProvider.ExecuteCmd(() => Connection, "dbo.MovieContentSelectByCID",
                null,
                    (reader, set) =>
                    {
                         movie = DataMapper<Movie>.Instance.MapToObject(reader);
                    });
            return movie;
        }

        public List<Movie> GetAll()
        {
            List<Movie> list = new List<Movie>();
            DataProvider.ExecuteCmd(() => Connection, "dbo.MovieContentSelect",
                null,
                    (reader, set) =>
                    {
                        Movie movie = DataMapper<Movie>.Instance.MapToObject(reader);
                        if (list == null) { list = new List<Movie>(); }
                        list.Add(movie);
                    });
            return list;
        }

        public List<Movie> FilterMoviesByCategory(string category, List<Movie> movies, string[] filters = null)
        {
            IEnumerable<Movie> filteredMovies = movies.Where(a =>
            {
                if (a.GetType().GetProperty(category) != null)
                {
                    if (category == "ReleaseYear") { return a.ReleaseYear == DateTime.Today.Year; }
                    else if (category == "Genre") { return a.Genre == category; }
                    else { return false; }
                }
                else { return false; }
            });
            return filteredMovies.ToList();
        }

    }
}