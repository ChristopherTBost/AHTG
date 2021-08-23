using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AHTG.Hospital.Web
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    /// <summary>
    /// Class for configuring the SPA during startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// used to throw a few sample entities in the newly created data store
        /// </summary>
        /// <param name="hospitalContext">newly created context</param>
        static void PopulateSampleData(ObjectModel.Context.HospitalContext hospitalContext)
        {
            hospitalContext.Add(new ObjectModel.Entities.Hospital() { Title = "Hospital 1" });
            hospitalContext.Add(new ObjectModel.Entities.Hospital() { Title = "Hospital 2" });
            hospitalContext.SaveChanges();
        }

        /// <summary>
        /// default constructor called by the pipeline
        /// </summary>
        /// <param name="configuration">configuration of the app</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// access to the app configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// called during app construction pipeline to add services. mostly from template
        /// </summary>
        /// <param name="services">the application wide IOC service container</param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // add the context service, understanding is this is a "per-request" lifetime manager by default
            services.AddDbContext<Hospital.ObjectModel.Context.HospitalContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("HospitalDbConnection")));

            // configure the repository pattern provider
            services.AddScoped<Hospital.ObjectModel.HospitalRepository>(
                (sp) => new ObjectModel.HospitalRepository(sp.GetRequiredService<Hospital.ObjectModel.Context.HospitalContext>()));

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// Template geenreated
        /// </summary>
        /// <param name="app">the application builder</param>
        /// <param name="env">the hosting environment</param>
        /// <param name="db">injected context</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Hospital.ObjectModel.Context.HospitalContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            /*
             * the following code is specific to this being an exercise
             * i wouldn't normally "create" a datastore this way.
             */
            #region database creation

            bool created = false;
            try
            {
                created = db.Database.EnsureCreated();
            }
            catch (System.Exception)
            {
                /*
                 * had some situations where an existing database was throwing Exceptions
                 */
                db.Database.EnsureDeleted();
                created = db.Database.EnsureCreated();
            }

            // only add sample data if newly created
            if (created)
                PopulateSampleData(db);

            #endregion database creation
        }
    }
}
