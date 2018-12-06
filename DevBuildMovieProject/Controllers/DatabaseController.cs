using DevBuildMovieProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult SaveNewMovie(Movie newMovie)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();


            if (newMovie.MovieTitle != null)
            {
                ORM.Movies.Add(newMovie);
                ORM.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditMovie(int MovieID)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            Movie found = ORM.Movies.Find(MovieID);
            return View(found);
        }

        public ActionResult SaveMovieChanges(Movie updatedMovie)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            Movie oldMovie = ORM.Movies.Find(updatedMovie.MovieID);

            oldMovie.MovieTitle = updatedMovie.MovieTitle;
            oldMovie.Genre = updatedMovie.Genre;
            oldMovie.Year = updatedMovie.Year;
            oldMovie.Watched = updatedMovie.Watched;

            ORM.Entry(oldMovie).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMovie(int MovieID)
        {
            DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
            Movie found = ORM.Movies.Find(MovieID);

            ORM.Movies.Remove(found);
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }

        //#region CRUD - Update Function
        //public ActionResult EditMovie(int MovieID)
        //{
        //    DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
        //    Movie found = ORM.Movies.Find(MovieID);
        //    return View(found);
        //}
        //public ActionResult SaveMovieUpdates(Movie updatedMovie)
        //{
        //    DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();
        //    Movie originalMovie = ORM.Movies.Find(updatedMovie.MovieID);

        //    if(originalMovie != null && ModelState.IsValid)
        //    {
        //        originalMovie.MovieTitle = updatedMovie.MovieTitle;
        //        originalMovie.Genre = updatedMovie.Genre;
        //        originalMovie.Year = updatedMovie.Year;
        //        //originalMovie.Watched = updatedMovie.Watched;

        //        ORM.Entry(originalMovie).State = System.Data.Entity.EntityState.Modified;
        //        ORM.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Oops! Something went wrong!";
        //        return View("Error");
        //    }
        //}
        //#endregion

        //#region CRUD - Delete Funciton
        //public ActionResult DeleteMovie(int MovieID)
        //{
        //    DevBuildMovieDBEntities ORM = new DevBuildMovieDBEntities();

        //    Movie found = ORM.Movies.Find(MovieID);

        //    ORM.Movies.Remove(found);
        //    ORM.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //#endregion
    }
}