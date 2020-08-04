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
    public static class SequenceExtensions
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if(source == null || source.Count() == 0 || action == null)
                return;
            
            foreach(var src in source)
                action(src);
        }
    }

    public static class ServiceExtensions
    {
        public static void SetupAuth0(this IServiceCollection services, IConfiguration conf)
        {
            var ao = conf.GetSection("Auth0").Get<Auth0>();
            var env = (AppEnv)conf.GetValue(typeof(AppEnv), "Env");
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = ao.Domain;
                o.Audience = ao.AppId;
                o.RequireHttpsMetadata = (env == AppEnv.Production);
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuthorization(o =>
            {
                ao.Policies
                    .AsEnumerable<string>()
                    .Each(p => {
                        HasScopeRequirement req = new HasScopeRequirement(p, ao.Domain);
                        o.AddPolicy(p, policy => policy.Requirements.Add(req));
                    });
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}