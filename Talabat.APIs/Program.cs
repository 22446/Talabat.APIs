using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helper;
using Talabat.APIs.MiddleWareErrors;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.DataSeedingIdentity;
using Talabat.Services;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<IdentityApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            builder.Services.AddScoped<ITokenServices, TokentServices>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPaymentSrevices, PaymentServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", option =>
                {
                    option.AllowAnyHeader();
                    option.AllowAnyMethod();
                    option.WithOrigins(builder.Configuration["FrontBaseUrl"]);

                });
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
               var connection= builder.Configuration.GetConnectionString("RedisConnection");
               return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.ServiceExtention();
            builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<IdentityApplicationDbContext>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Assuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"]))
                };
            });
            var app = builder.Build();
            #region UpdateDataBase
            using var Scope = app.Services.CreateScope();
            var Servces = Scope.ServiceProvider;
            var LoggerFactory = Servces.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = Servces.GetRequiredService<ApplicationDbContext>();
                var DbContextIdentity = Servces.GetRequiredService<IdentityApplicationDbContext>();
                var UserManger = Servces.GetRequiredService<UserManager<AppUser>>();
                await DbContext.Database.MigrateAsync();
                await DbContextIdentity.Database.MigrateAsync();
                await ApplicationDbContextSeeding.DataseedAsync(DbContext);

                await IdentityDataSeeding.DataSeeding(UserManger);

            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "Theres an error happing During The Migrate");
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            app.UseMiddleware<ApiInternalServerError>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}