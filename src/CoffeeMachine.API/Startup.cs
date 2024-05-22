﻿using System.Text;
using CoffeeMachine.API.Middlewares;
using CoffeeMachine.Application.Extensions;
using CoffeeMachine.Persistence.Extentions;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace CoffeeMachine.API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        
        var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File($"{Environment.CurrentDirectory}/Logs/{DateTime.UtcNow:yyyy/dd/MM}.txt")
            .CreateLogger();

        logger.Information("Starting web host");
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddApplicationCore();

        services.AddInfrastructure(Configuration);

        services.AddEndpointsApiExplorer();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true; 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetSection("GenerateTokenSettings:MyAuthServer").Value,
                    ValidAudience = Configuration.GetSection("GenerateTokenSettings:MyAuthClient").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration.GetSection("GenerateTokenSettings:Secret").Value))
                };
            });
        
        services.AddAuthorization();

        services.AddSwaggerGen();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSerilogRequestLogging();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCustomExceptionHandler();
        
        app.UseRouting();
        
        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}