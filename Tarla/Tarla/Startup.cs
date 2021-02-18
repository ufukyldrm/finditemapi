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
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;
using System.Configuration;
using MySqlConnector;

namespace Tarla
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
            services.AddTransient(_ => new ApplicationDatabaseObject(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddControllers();
       

            //   services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            /* interfaceleri ve helperlarý her yerde kullanabilmek için bu programda kullanmýyorsun.
            services.AddScoped<Helpers.CloudinarySettings>();
            services.AddScoped<Data.AuthRepository>();
            services.AddScoped<Data.AppRepository>();
            services.AddScoped<Data.IAppRepository, Data.AppRepository>();
            */

            ////hazýr kod


            ////hazýr kod

            ////tokenn

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateAudience = true,
                    ValidAudience = "heimdall.fabrikam.com",
                    ValidateIssuer = true,
                    ValidIssuer = "west-world.fabrikam.com",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("uzun ince bir yoldayým þarkýsýný buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ctx => {
                        //Gerekirse burada gelen token içerisindeki çeþitli bilgilere göre doðrulam yapýlabilir.
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx => {
                        Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });




            ////tokenn

            ////hazýr kod















        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
