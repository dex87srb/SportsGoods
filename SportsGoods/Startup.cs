using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsGoods.Models;

namespace SportsGoods
{
    public class Startup {
   public Startup(IConfiguration config) => Configuration = config; //allows to read the connection string from appconfiguration.json file
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) { //depedency injection via method injection
            services.AddMvc();
            // services.AddSingleton<IRepository, DataRepository>(); //means that a single object from DataRepository will be created the first time to resolve a all dependencies on a IRepository interface
            services.AddTransient<IRepository, DataRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            string conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(conString)); //this method is used to set up Context class and which database provider to use through UseSqlServer method

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) { //middleware or chain of middlewares
            app.UseDeveloperExceptionPage(); //ovo je sve request pipeline 
            app.UseStatusCodePages(); //middleware u request pipeline
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
