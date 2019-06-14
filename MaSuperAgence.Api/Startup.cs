using AutoMapper;
using MaSuperAgence.Api.Models;
using MaSuperAgence.Api.Models.Dao;
using MaSuperAgence.Api.Models.DaoImpl.SQLServer;
using MaSuperAgence.Api.Models.Entities.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MaSuperAgence.Api
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Default configuration.
      services
        .AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
        .AddJsonOptions(l =>
        {
          l.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

      // Set JWT authentification.
      services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            // What to valide
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            // Setup validate data
            ValidIssuer = "projects.in",
            ValidAudience = "readers",
            IssuerSigningKey = Methods.GetSymmetricSecurityKey(
                                  Configuration.GetSection("AppSettings:SecurityKey").Value
                                )
          };
        });

      // Set context
      services
        .AddDbContext<DataBaseContext>(options =>
        {
          options.UseSqlServer(
            Configuration.GetConnectionString("ConnectionToDbSQLServerLocal")
          );
        }
      );

      // Add independency injection
      services
        .AddScoped<IPhotoDao, PhotoDaoImpl>()
        .AddScoped<IUserDao, UserDaoImpl>()
        .AddScoped<IPropertyDao, PropertyDaoImpl>();

      // Inject AutoMapper
      services
        .AddAutoMapper();
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
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      //app.UseHttpsRedirection();
      //app.UseMvc();
      app.UseCors(l =>
      {
        l.AllowAnyOrigin();
        l.AllowAnyMethod();
        l.AllowAnyHeader();
      });
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
