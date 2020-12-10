using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.infrastructure.Data;
using MovieShop.infrastructure.Repositories;
using MovieShop.infrastructure.Services;
using MovieShop.Infrastructure.Services;

namespace MovieShop.API
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
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<ICastRepository, CastRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAsyncRepository<Genre>, EfRepository<Genre>>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAsyncRepository<MovieGenre>, EfRepository<MovieGenre>>();
            services.AddScoped<IAsyncRepository<Favorite>, EfRepository<Favorite>>();
            services.AddScoped<IAsyncRepository<Review>, EfRepository<Review>>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>(); 
            services.AddScoped<ICryptoService,CryptoService>();
            services.AddDbContext<MovieShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(("MovieShopDbConnection"))));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieShop.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieShop.API v1"));
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
