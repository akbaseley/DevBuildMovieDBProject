using DevBuildMovieProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBuildMovieProject.Data
{
    public class AppUserdbContext : IdentityDbContext<AppUser>
    {
        //we need to use IdentityDbContext to connect our Database & our Identity tables
        //& this is a way to do it using AppUser
       public AppUserdbContext(string connectionString): base(connectionString) { }

    }
}