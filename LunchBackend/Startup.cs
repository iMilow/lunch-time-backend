using LunchBackend.DbAccess;
using LunchBackend.DbAccess.Interfaces;
using LunchBackend.DbAccess.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace LunchBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            // Cors
            services.AddCors();
            
            services.AddDbContext<ItIsLunchTimeContext>(options => {
                options.UseInMemoryDatabase(databaseName: "TestDb");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // AutoMapper
            services.AddAutoMapper();

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Delivery>, Repository<Delivery>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseMvc();
        }
    }
}
