using Convention.WebApi.Api.Areas.Administration;
using Convention.WebApi.DataAccess;
using Convention.WebApi.Middleware;
using Convention.WebApi.Security;
using Convention.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Convention.WebApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAdminPanel",
                    builder =>
                        builder
                            .WithOrigins(Configuration["AdminPanel:Url"])
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

            services.AddControllers();

            // Add bearer authentication
            var domain = $"https://{Configuration["Auth0:Domain"]}/";
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = Configuration["Auth0:Audience"];
                });

            // Setup authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("create:avenues", policy => 
                    policy.Requirements.Add(new HasScopeRequirement("create:avenues", domain)));
            });

            // Add generate Swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Convention API", Version = "v1" });

                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Add bearer token below.",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            // Add authorization handlers
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            // Register database contexts
            services.AddDbContext<ConventionDbContext>(options => options.UseSqlite("Data source=convention.db"));

            // Register services
            services.AddScoped<IAdminServices, DomainAdminServices>();
            
            // Register repositories
            services.AddScoped<IAvenueRepository, AvenueRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ConventionDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                // Migrate db schema on startup
                dbContext.Database.Migrate();

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Convention API v1");
                    c.DocumentTitle = "Convention API v1";
                });
            }
            else
            {
                // Handle errors gracefully
                app.UseMiddleware<ErrorHandlerMiddleware>();
            }

            // Log all requests
            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
