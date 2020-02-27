using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using LoggerService;
using TaosPerformanceAPI.Mapping;
using TaosPerformanceAPI.DAL;
using TaosPerformanceAPI.Security;
using TaosPerformanceAPI.Common;

namespace TaosPerformanceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB Context Set up
            String cnxString = Configuration.GetValue<String>("dbInUse");

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Services
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IResponseFormatter, ResponseFormatter>();
            services.AddScoped<IRequestHandler, RequestHandler>();
            services.AddScoped<MySQLContext>();
            services.AddScoped<TaosPerformanceDB>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<SecurityMiddleware>();
            services.AddScoped<TokenProvider>();

            AutoMapperConfiguration.Configure();

            // CORS
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyOrigin()
                );
            });

            services.AddAuthentication().AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:SecurityKey"])),
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSession(options => {
                options.Cookie.Name = ".Taos.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(45);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder => {
                    appBuilder.Use(async (context, next) => {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        var error = context.Features[(typeof(IExceptionHandlerFeature))] as IExceptionHandlerFeature;

                        if (error != null && error.Error is SecurityTokenExpiredException)
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                State = "Unauthorized",
                                Msg = "token expired"
                            }));
                        }
                        else if (error != null && error.Error != null)
                        {
                            context.Response.StatusCode = 500;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                State = "Error",
                                Msg = error.Error.Message
                            }));
                        }
                        else await next();
                    });
                });
            }

            app.UseMvc(routes => {
                routes.MapRoute(name: "status", template: "{controller=Auth}/{action=status}/{id?}");
            });
        }
    }
}
