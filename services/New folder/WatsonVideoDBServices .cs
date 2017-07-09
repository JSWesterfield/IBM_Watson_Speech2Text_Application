using ATTeamE.web.Interfaces.Services;
using ATTeamE.web.Models;
using ATTeamE.web.Interfaces.Sql;
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

        public Movie Get(int id)
        {
            throw new NotImplementedException();
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
            list.ForEach(a =>
            {
                a = Get(a.CID);
            });

            throw new NotImplementedException();
        }

        //public string GetResponseMessage(int CID)
        //{
        //    //Movie movie = null;
        //    List<Movie> list = null;
        //    DataProvider.ExecuteCmd(() => Connection, "dbo.MovieContentSelectByCID",
        //        param => param.Add(new SqlParameter("CID)),
        //        (reader, set) =>
        //        {
        //            movie = DataMapper<Movie>.Instance.MapToObject(reader);
        //        });

        //    return movie;
        //}

        //public List<Movie> GetByCID(int CID)
        //{
        //    List<Movie> list = null;

        //    Action<SqlParameterCollection> inputMapper = delegate (SqlParameterCollection parameters)
        //    {
        //        parameters.AddWithValue("CID", CID);
        //    };

        //    Action<IDataProvider, short> resultMapper = delegate (IDataProvider reader, short set)
        //    {
        //        Movie movie = new Movie();

        //        int startingIndex = 0;
        //        movie.CID = reader.GetSafeString(startingIndex++);
        //        movie.Content_Title = reader.GetSafeString(startingIndex++);
        //        movie.Poster = reader.GetSafeString(startingIndex++);
        //        movie.Release_Year = reader.GetSafeInt32(startingIndex++);
        //        movie.Genre = reader.GetSafeString(startingIndex++);

        //        list.Add(movie);
        //    };

        //    DataProvider.ExecuteCmd(() => Connection,
        //            "dbo.MovieContentSelectByCID",
        //            inputMapper,
        //            resultMapper
        //     );

        //    return list;
        //}

        public List<Movie> GetByCID(int CID)
        {
            List<Movie> list = null;
            DataProvider.ExecuteCmd(() => Connection, "dbo.MovieContentSelectByCID",
                param => param.Add(new SqlParameter("CID", CID)),
                (reader, set) =>
                {
                    movie = DataMapper<Movie>.Instance.MapToObject(reader);
                });
        }

            return movie;

        }

    public int Insert(object model)
        {
            throw new NotImplementedException();
        }

        public void Update(object model)
        {
            throw new NotImplementedException();
        }


        
    }
}