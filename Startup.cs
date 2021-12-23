using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using RestNew.Models;
using Npgsql;

namespace RestNew
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:5001");
                                });
            });


            services.AddControllers().AddNewtonsoftJson();

            string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};",
                    "codeboxx-postgresql.cq6zrczewpu2.us-east-1.rds.amazonaws.com",
                    "codeboxx",
                    "ZiWangChen",
                    "5432",
                    "Codeboxx1!");

            using var conn = new NpgsqlConnection(connString);

            services.AddDbContext<PostgreApplicationContext>(options => options.UseNpgsql(connString));

            // MySQL Connection String

            // NOTE: USE ACTUAL CONNECTION STRING IN CONNECTION FOR PRODUCTION WHEN DEPLOYING!!!
            // var connectionString = Environment.GetEnvironmentVariable("DEFAULT__ENVIRONMENT");
            var connectionString = "server=codeboxx.cq6zrczewpu2.us-east-1.rds.amazonaws.com;port=3306;database=ZiWangChen;uid=codeboxx;password=Codeboxx1!";


            // Replace with your server version and type.
            // Use 'MariaDbServerVersion' for MariaDB.
            // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
            // For common usages, see pull request #1233.
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));


            // Replace 'YourDbContext' with the name of your own DbContext derived class.
            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
