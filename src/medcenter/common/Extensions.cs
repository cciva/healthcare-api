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
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IO;

namespace MedCenter
{
    public static class ServiceExtensions
    {
        public static void SetupAuth(this IServiceCollection services, IConfiguration conf)
        {
            string domain = $"https://{conf["Auth0:Domain"]}/";
            string appid = conf["Auth0:ApiIdentifier"];
            IConfigurationSection ps = conf.GetSection("Auth0:Policies");
            string[] policies = ps.Get<string[]>();

            services.Configure<AuthOptions>(o => {
                conf.GetSection("Auth0").Bind(o);
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = domain;
                o.Audience = appid;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("read:medrecords", policy => policy.Requirements.Add(new HasScopeRequirement("read:medrecords", domain)));
                o.AddPolicy("modify:medrecords", policy => policy.Requirements.Add(new HasScopeRequirement("modify:medrecords", domain)));
                o.AddPolicy("read:exams", policy => policy.Requirements.Add(new HasScopeRequirement("read:exams", domain)));
                o.AddPolicy("arrange:exams", policy => policy.Requirements.Add(new HasScopeRequirement("arrange:exams", domain)));
                o.AddPolicy("cancel:exams", policy => policy.Requirements.Add(new HasScopeRequirement("cancel:exams", domain)));
                o.AddPolicy("fetch:medrecord", policy => policy.Requirements.Add(new HasScopeRequirement("fetch:medrecord", domain)));
                o.AddPolicy("admin:status", policy => policy.Requirements.Add(new HasScopeRequirement("admin:status", domain)));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}