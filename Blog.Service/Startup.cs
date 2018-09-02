using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Blog.Client.Services;
using Blog.Data;
using Blog.Data.Interfaces;
using Blog.Data.Mocks;
using Blog.Data.Repositories;
using Blog.Data.Repositories.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Service
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
            // Data repositories
            //services.AddSingleton<IPostsRepository, PostsRepositoryMock>();
            //services.AddSingleton<IUsersRepository, UsersRepositoryMock>();
            services.AddSingleton(typeof(MongoDbSettings));
            services.AddSingleton<IPostsRepository, PostsRepositoryMongo>();
            services.AddSingleton<IUsersRepository, UsersRepositoryMongo>();
            services.AddSingleton(typeof(RepositoryFacade));

            // Client services
            services.AddHttpClient<AuthService>();
            services.AddHttpClient<PostsService>();
            services.AddHttpClient<UsersService>();

            // Set JWT authentication
            var authParameters = new AuthParameters();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = authParameters.GetSymmetricSecurityKey(),
                    ValidAudience = authParameters.Audience,
                    ValidateAudience = true,
                    ValidIssuer = authParameters.Issuer,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                // Custom error handler
                app.UseExceptionHandler(options =>
                {
                    options.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "text/html";
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                            await context.Response.WriteAsync(err).ConfigureAwait(false);
                        }
                    });
                });
            }
            app.UseStatusCodePages();

            //app.UseMvc();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=login}/{action=index}/{id?}");
            });
        }
    }
}
