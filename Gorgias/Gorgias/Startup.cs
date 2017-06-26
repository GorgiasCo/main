using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Gorgias.Migrations;
using System.Data.Entity;
using Gorgias.DataLayer.Authentication;

[assembly: OwinStartup(typeof(Gorgias.Startup))]

namespace Gorgias
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, Configuration>());
            //app.UseCors(System.Web.Cors.CorsOptions.AllowAll);
        }
    }
}
