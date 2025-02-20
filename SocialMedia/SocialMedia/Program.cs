using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle;
using Scalar.AspNetCore;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["AppSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                    ValidateIssuerSigningKey = true
                };
            });

            // Add services to the container.
            builder.Services.AddDbContext<SocialDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("local")));

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();

            builder.Services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<SocialDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseSwagger(opt =>
            {
                opt.RouteTemplate = "openapi/{documentName}.json";
            });
            app.MapScalarApiReference(opt =>
            {
                opt.Title = "Scalar Example";
                opt.Theme = ScalarTheme.Mars;
                opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
            });
            app.UseHttpsRedirection();

            app.MapControllers();
            app.UseAuthorization();
             

            app.Run();
        }
    }
}
