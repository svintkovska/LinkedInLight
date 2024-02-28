using BLL.DTOs;
using BLL.Interfaces;
using BLL.Mapper;
using BLL.Services;
using DLL.Data;
using DLL.Models;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.Stores.MaxLengthForKeys = 128;
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();


builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ISmtpEmailService, SmtpEmailService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();


var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<String>("JWTSecretKey")));

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
	cfg.RequireHttpsMetadata = false;
	cfg.SaveToken = true;
	cfg.TokenValidationParameters = new TokenValidationParameters()
	{
		IssuerSigningKey = signinKey,
		ValidateAudience = false,
		ValidateIssuer = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ClockSkew = TimeSpan.Zero
	};
});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();






var app = builder.Build();
app.UseCors(options =>
				options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
if (!Directory.Exists(dir))
{
	Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(dir),
	RequestPath = "/images"
});


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
