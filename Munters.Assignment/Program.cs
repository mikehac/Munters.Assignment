
using AutoMapper;
using Munters.Assignment.Extentions;
using Munters.Assignment.Middleware;
using Munters.Assignment.ServicesConfig;

namespace Munters.Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddMemoryCache();

            // Add services to the container.
            builder.Services.AppSettingsConfig(builder.Configuration);
            builder.Services.AddHttpClient();
            builder.Services.RegisterServices();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            IMapper mapper = MapperConfig.RegisterMaps().CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandler>();

            app.MapControllers();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.Run();
        }
    }
}