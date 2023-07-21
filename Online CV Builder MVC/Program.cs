
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Online_CV_Builder_MVC.JwtTokenHandler;
using Online_CV_Builder_MVC.WebApiClient;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor
builder.Services.AddTransient<JwtTokenHandler>(); // Add JwtTokenHandler
builder.Services.AddHttpClient<WebApiClient>()
	.AddHttpMessageHandler<JwtTokenHandler>(); // Add the JwtTokenHandler to the HttpClient

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				ValidAudience = "online-cv-builder-app",
				ValidIssuer = "your-application-domain.com",
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("q3WQ2uRT@e0!$bnS"))
			};

			// Additional JWT bearer options configuration, if needed
			// For example: options.RequireHttpsMetadata = false;
		});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
