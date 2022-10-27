using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using System.Text;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers()
                .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration.GetSection("Secrets:JwtIssuer").Value,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration.GetSection("Secrets:JwtAudience").Value,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Secrets:JwtSecret").Value)),
                };
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}