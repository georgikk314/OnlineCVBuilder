using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder;
using Online_CV_Builder.Configuration;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Models;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ResumeBuilderContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseLazyLoadingProxies();
            }
        );
        builder.Services.AddDataProtection();
        // builder.Services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);   
        builder.Services.AddCors
            ( o => { o.AddPolicy
                ("AllowAny", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        builder.Services.AddAutoMapper(typeof(MapperInitialization));
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperInitialization>();
        });
        builder.Services.AddSingleton(mapperConfiguration.CreateMapper());
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthorization();
        // app.UseEndpoints(endpoints => { });
        app.UseAuthentication();
        app.MapControllers();
        app.MapRazorPages();
        app.UseExceptionHandler();
        app.UseSerilogRequestLogging();
        // app.MapUsersEndpoints();
        app.UseCors("AllowAny");
        app.UseRouting();
        //app.UseUrls("https://localhost:<some port>/");
        app.Run();
    }
}