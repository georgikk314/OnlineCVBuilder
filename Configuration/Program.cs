using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Services;
using Online_CV_Builder.MappingProfiles;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Configure JWT authentication
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
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("q3WQ2uRT@e0!$bnS"))
    };
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<ResumeMappingProfile>();
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<ITemplateDownloadService, TemplateDownloadService>();
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IResumeSharingService, ResumeSharingService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

