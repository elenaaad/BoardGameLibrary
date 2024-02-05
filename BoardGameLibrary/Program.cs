using AutoMapper;
using BoardGameLibrary.Helpers.JwtUtils;
using BoardGameLibrary.Services.AdminService;
using BoardGameLibrary.Services.UserService;
using DAL.Data;
using DAL.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using sib_api_v3_sdk.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using BoardGameLibrary.Services.CollectionService;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using BoardGameLibrary.Profiles;
using Microsoft.Extensions.Options;
using BoardGameLibrary.Helpers.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BoardGameLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// Add services to the container.

var allowOrigins = "_allowOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BoardGameLibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BoardGameLibrary")));

builder.Services.AddScoped<IBoardGameRepository, BoardGameRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
         .RequireAuthenticatedUser()
         .RequireRole("User", "Admin")
        .Build();
});

//cors

builder.Services.AddCors(options => {
    options.AddPolicy(name: allowOrigins,
        builder => {
            builder.WithOrigins("http://localhost:7183")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            ;
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("JWT:Key").Value!))

    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapFallbackToFile("index.html"); ;

app.Run();



