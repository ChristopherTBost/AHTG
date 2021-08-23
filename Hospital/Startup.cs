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

    public class Startup
    {
        static void PopulateSampleData(ObjectModel.Context.HospitalContext hospitalContext)
        {
            hospitalContext.Add(new ObjectModel.Entities.Hospital() { Title = "Hospital 1" });
            hospitalContext.Add(new ObjectModel.Entities.Hospital() { Title = "Hospital 2" });
            hospitalContext.SaveChanges();
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddDbContext<Hospital.ObjectModel.Context.HospitalContext>(

#if IN_MEMORY_CONTEXT
                options => 
                options.UseSqlite(new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = ":memory:" }.ToString()));
//                options.UseInMemoryDatabase(Configuration["TargetDatabaseName"])) ; ;
#else
                options => options.UseSqlServer(Configuration.GetConnectionString("HospitalDbConnection"))
                );
#endif


            services.AddScoped<Hospital.ObjectModel.HospitalRepository>((sp) => new ObjectModel.HospitalRepository( sp.GetRequiredService<Hospital.ObjectModel.Context.HospitalContext>()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            bool created = false;
            try
            {
                created = db.Database.EnsureCreated();
            } catch (System.Exception e)
            {
                db.Database.EnsureDeleted();
                created = db.Database.EnsureCreated();
            }
            if (!created)
                System.Diagnostics.Debug.WriteLine("Database exists");
            else
                PopulateSampleData(db);

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

        }
    }
}
