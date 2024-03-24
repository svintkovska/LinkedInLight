using BLL.Interfaces;
using BLL.Services;
using DLL.Data;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using SendGrid.Helpers.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using BLL.Utilities;
using BLL.Utilities.SignalR;
using AutoMapper;
using DLL.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

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


builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAdditionalProfileService, AdditionalProfileService>();
builder.Services.AddScoped<IRecommendedProfileService, RecommendedProfileService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IPrivacySettingsService, PrivacySettingsService>();
builder.Services.AddScoped<IProfileSecurityService, ProfileSecurityService>();


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

builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGridOptions"));
builder.Services.AddTransient<ISendGridService, SendGridService>();
builder.Services.AddSignalR();


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

var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(dir))
	Directory.CreateDirectory(dir);
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(dir),
	RequestPath = "/uploads"
});

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(dir),
	RequestPath = "/images"
});


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.SeedData();

app.Run();
