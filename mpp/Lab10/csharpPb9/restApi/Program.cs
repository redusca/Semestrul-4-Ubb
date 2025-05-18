
using csharpPb9.utils;
using Microsoft.EntityFrameworkCore;
using persistene.repository.context;
using Scalar.AspNetCore;

namespace restApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<Context>(options =>
               options.UseSqlite(builder.Configuration.GetConnectionString("TriatlonDB")));
            builder.Services.AddScoped<IProbaRepository, ContextProbaRepo>();
            builder.Services.AddScoped<IProbaService, ProbaService>();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
