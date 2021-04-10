using Infrastructure.Data;
using Infrastructure.Filters;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<MovieShopLogFilter>();

            services.AddControllers(options =>
            {
                options.Filters.Add(new MovieShopHeader("Company", "Movie Shop Inc."));
                options.Filters.Add(typeof(MovieShopLogFilter)); // By type
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MovieShop.API", Version = "v1"});
            });
            services.AddDbContext<MovieShopDbContext>(options =>
                options.UseSqlServer(Configuration
                    .GetConnectionString("MovieShopDbConnection")));
            services.AddAutoMapper(typeof(Startup), typeof(MovieShopMappingProfile));
            ConfigureDependencyInjection(services);
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();
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
                    .AllowAnyMethod().AllowCredentials();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}