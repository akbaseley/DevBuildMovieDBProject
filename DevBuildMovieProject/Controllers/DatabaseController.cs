using DevBuildMovieProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevBuildMovieProject.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            ViewBag.MovieList = ORM.Movies.ToList();

            return View();
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        public ActionResult SaveNewMovie(Movie newMovie, Director newDirector)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            List<Director> directors = ORM.Directors.Where(c => c.LastName.Equals(newDirector.LastName)
                                              && c.FirstName.Equals(newDirector.FirstName)).ToList();

            if (newDirector.LastName != null && directors.Count == 0)
            {
                    ORM.Directors.Add(newDirector);
                    ORM.SaveChanges();

            }
            else
            {
                newDirector = ORM.Directors.First(c => c.LastName.Equals(newDirector.LastName)
                                  && c.FirstName.Equals(newDirector.FirstName));
            }


            if (newMovie != null)
            {
                newMovie.Director = newDirector;
                ORM.Movies.Add(newMovie);
                ORM.SaveChanges();
            }




            return RedirectToAction("Index");
        }
    }
}