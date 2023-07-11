using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.MappingProfiles;
using Online_CV_Builder.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
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

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<ResumeMappingProfile>();
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper());


builder.Services.AddScoped<IResumeService, ResumeService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
