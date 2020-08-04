using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Contracts;
using MediatR;

namespace MedCenter
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
            services.AddOptions();
            services.Configure<Auth0>(Configuration.GetSection("Auth0"));
            services.SetupAuth0(Configuration);

            // V2 improvements
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.Configure<RouteOptions>(o =>
            {
                o.ConstraintMap.Add("donationType", typeof(DonationTypeConstraint));
            });

            services.AddApiVersioning(v =>
            {
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // V2 improvements :
            // we are now able to send request down to its handler for 
            // further processing
            services.AddTransient<IRequestHandler<CreateTherapyCommand, Therapy>, CreateTherapyHandler>();
            // handlers for interaction with another REST api
            services.AddTransient<IRequestHandler<BuyMedicinesCommand, PharmacyInvoice>, BuyMedicinesHandler>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-dev");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UsePathBase("/medcenter/api");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
