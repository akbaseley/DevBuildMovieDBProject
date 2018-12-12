using DevBuildMovieProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using DevBuildMovieProject.Data;

namespace DevBuildMovieProject
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {   //creating connectionString that will connection our Identity to our database
            const string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=DevBuildMovieDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //creating an instance of the AppUserdbContext
            app.CreatePerOwinContext(() => new AppUserdbContext(connectionString));

            //creating an instance of UserStore (built in) on the extended AppUser 
            //because UserStore has the functions needed for Identity 
            //to save and make edits to the database
            app.CreatePerOwinContext<UserStore<AppUser>>((options, context) => 
                new UserStore<AppUser>(context.Get<AppUserdbContext>()));

            //creating an instance of UserManager using parameter of UserStore
            //all the functions are happening on UserStore & this will manager those functions
            //throughout the program -- UserManager manages the User
            app.CreatePerOwinContext<UserManager<AppUser>>(
                (options, context) => new UserManager<AppUser>(context.Get<UserStore<AppUser>>()));

            // "<>" states what TYPE of that object we're using.
            //creating an instance of RoleManager using parameters of RoleManager and RoleStore
            app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
                new RoleManager<AppRole>(
                    new RoleStore<AppRole>(context.Get<AppUserdbContext>())));
                                            //using Microsoft.ASPNET.Identity.Owin;
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Identity/Login"),
            });

            
            //NEXT STEP: ADD LINES TO web.config
        }
    }
}