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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IO;

namespace MedCenter.Tmp
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
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            string appid = Configuration["Auth0:ApiIdentifier"];
            IConfigurationSection ps = Configuration.GetSection("Auth0:Policies");
            string[] policies = ps.Get<string[]>();

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("donationType", typeof(DonationTypeConstraint));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = appid;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:medrecords", policy => policy.Requirements.Add(new HasScopeRequirement("read:medrecords", domain)));
                options.AddPolicy("modify:medrecords", policy => policy.Requirements.Add(new HasScopeRequirement("modify:medrecords", domain)));
                options.AddPolicy("read:exams", policy => policy.Requirements.Add(new HasScopeRequirement("read:exams", domain)));
                options.AddPolicy("arrange:exams", policy => policy.Requirements.Add(new HasScopeRequirement("arrange:exams", domain)));
                options.AddPolicy("cancel:exams", policy => policy.Requirements.Add(new HasScopeRequirement("cancel:exams", domain)));
                options.AddPolicy("fetch:medrecord", policy => policy.Requirements.Add(new HasScopeRequirement("fetch:medrecord", domain)));
                options.AddPolicy("admin:status", policy => policy.Requirements.Add(new HasScopeRequirement("admin:status", domain)));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddApiVersioning(v =>
            {
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    
        // private AppOptions LoadConf(IServiceCollection services)
        // {
        //     var configBuilder = new ConfigurationBuilder()
        //             .SetBasePath(Directory.GetCurrentDirectory())
        //             .AddJsonFile("appsettings.json", optional: true);
        //             var config = configBuilder.Build();

        //     services.Configure<AppOptions>(config);
        // }
    }
}
