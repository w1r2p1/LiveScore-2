using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LiveScore.Utils.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.IO;
using System.Reflection;
using Autofac.Core;
using LiveScore.Utils.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using LiveScore.Utils.ModelBinding;
using LiveScore.Utils.Middleware;

namespace LiveScore
{
    /// <summary>
    /// Startup class that provides application initialization and configuration functionalities.
    /// </summary>
    public class Startup
    {
        /// <summary>Application configuration</summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor that receives hosting environment.
        /// </summary>
        /// <param name="env">Hosting environment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile("config.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        /// <summary>
        /// This method configures application services.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns><see cref="AutofacServiceProvider"/></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddOptions();
            services.Configure<LiveScore.Utils.Config.Options>(Configuration);

            services.AddMvc(options =>
            {
                var arrayModelBinderProvider = options.ModelBinderProviders.OfType<ArrayModelBinderProvider>().First();
                options.ModelBinderProviders.Insert(
                    options.ModelBinderProviders.IndexOf(arrayModelBinderProvider),
                    new DelimitedArrayModelBinderProvider());
            });

            string domain = $"https://{Configuration["Auth0:Domain"]}/";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:scores", policy => policy.Requirements.Add(new HasScopeRequirement("read:scores", domain)));
                options.AddPolicy("create:scores", policy => policy.Requirements.Add(new HasScopeRequirement("create:scores", domain)));
            });

            var containerBuilder = RegisterModules();
            containerBuilder.Populate(services);

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        /// <summary>
        /// This method configures application and middleware.
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }

        private ContainerBuilder RegisterModules()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultModule());

            var path = Configuration["pluginPath"];

            if (String.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                return builder;
            }

            var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom);

            foreach (var assembly in assemblies)
            {
                var modules = assembly.GetTypes()
                    .Where(p => typeof(IModule).IsAssignableFrom(p) && !p.IsAbstract)
                    .Select(p => (IModule)Activator.CreateInstance(p));

                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            }

            return builder;
        }
    }
}
