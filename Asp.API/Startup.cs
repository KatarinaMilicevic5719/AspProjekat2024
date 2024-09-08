using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Asp.API.Core;
using Asp.API.Extensions;
using Asp.Application.Emails;
using Asp.Application.Logging;
using Asp.Application.UseCases;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation;
using Asp.Implementation.Email;
using Asp.Implementation.Emails;
using Asp.Implementation.Logging;
using Asp.Implementation.UseCases.Commands;
using Asp.Implementation.UseCases.Queries.EF;
using Asp.Implementation.UseCases.UseCaseLoggers;
using Asp.Implementation.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Asp.API
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
            var settings = new AppSettings();
            Configuration.Bind(settings);

            services.AddSingleton(settings);
            services.AddApplicationUser();
            services.AddJwt(settings);
            services.AddAspDbContext();
            services.AddUseCases();
           // services.AddTransient<AspDbContext>();
            services.AddTransient<IExceptionLogger1, ConsoleExceptionLogger1>();
           //services.AddTransient<IApplicationUser>();
            services.AddTransient<IExceptionLogger, DbExceptionLogger>();
            services.AddTransient<IUseCaseLogger, DbUseCaseLogger>();
            services.AddTransient<IEmailSender>(x =>new SmtpEmailSender(settings.EmailFrom, settings.EmailPassword)); ;
            services.AddTransient<UseCaseHandler>();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asp.API", Version = "v1" });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asp.API v1"));
            }

            app.UseRouting();
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
