using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using BoardGameLibrary.Helpers;
using DAL.Repositories;
using BoardGameLibrary.Services.UserService;
using BoardGameLibrary.Services.BoardGameService;
using BoardGameLibrary.Helpers.JwtUtils;
using BoardGameLibrary.Helpers.Middleware;
using DAL.Repositories.UnitOfWork;
using Microsoft.OpenApi.Models;

var allowOrigins = "_allowOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddRepositories();
//builder.Services.AddServices();
//builder.Services.AddUtils();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<BoardGameLibraryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//cors

builder.Services.AddCors(options => {
    options.AddPolicy(name: allowOrigins,
        builder => {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            ;
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseEndpoints(endpoints =>
//{ endpoints.MapControllers(); });

app.UseHttpsRedirection();

app.UseCors(allowOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();



///////////////////////////////////////////
/////

//using AutoMapper;
//using BoardGameLibrary.Helpers.JwtUtils;
//using BoardGameLibrary.Services.AdminService;
//using BoardGameLibrary.Services.UserService;
//using DAL.Data;
//using DAL.Repositories.UnitOfWork;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.EntityFrameworkCore;
//using sib_api_v3_sdk.Client;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Swashbuckle.AspNetCore.SwaggerUI;
//using Swashbuckle.AspNetCore.Swagger;
//using BoardGameLibrary.Services.CollectionService;
//using DAL.Repositories;
//using Microsoft.AspNetCore.Builder;
//using BoardGameLibrary.Profiles;
//using Microsoft.Extensions.Options;
//using BoardGameLibrary.Helpers.Middleware;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using BoardGameLibrary.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.OpenApi.Models;

//// Add services to the container.

//var builder = WebApplication.CreateBuilder(args);

//var allowOrigins = "_allowOrigins";

//builder.Services.AddRazorPages();
//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<BoardGameLibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BoardGameLibrary")));

//builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<IBoardGameRepository, BoardGameRepository>();
//builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
//builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IJwtUtils, JwtUtils>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

////builder.Services.AddAuthorization(options =>
////{
////    options.DefaultPolicy = new AuthorizationPolicyBuilder()
////         .RequireAuthenticatedUser()
////         .RequireRole("User", "Admin")
////        .Build();
////});


//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("3.1.0", new OpenApiInfo { Title = "My API", Version = "3.1.0" });
//});

////cors

//builder.Services.AddCors(options => {
//    options.AddPolicy(name: allowOrigins,
//        builder => {
//            builder.WithOrigins("http://localhost:44410")
//            .AllowAnyMethod()
//            .AllowAnyHeader()
//            .AllowCredentials()
//            ;
//        });
//});

////builder.Services.AddAuthentication(options =>
////{
////    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////}).AddJwtBearer(options =>
////{
////    options.TokenValidationParameters = new TokenValidationParameters
////    {
////        ValidateIssuerSigningKey = true,
////        ValidateAudience = false,
////        ValidateIssuer = false,
////        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
////            builder.Configuration.GetSection("JWT:Key").Value!))

////    };
////});


//var app = builder.Build();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseHsts();
//}


//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BoardGameLibrary API");
//        c.RoutePrefix = string.Empty;
//    });
//}

////var configuration = new ConfigurationBuilder()
////                .SetBasePath(Directory.GetCurrentDirectory())
////                .AddJsonFile("appsettings.json")
////                .Build();


////app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseCors("AllowAll");
//app.UseAuthentication();
//app.UseAuthorization();
////app.MapRazorPages();
//app.UseMiddleware<JwtMiddleware>();


////aici
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BoardGameLibrary API");
//    c.RoutePrefix = string.Empty;
//});

////var mapperConfig = new MapperConfiguration(mc =>
////{
////    mc.AddProfile(new MappingProfile());
////});

////IMapper mapper = mapperConfig.CreateMapper();
////app.UseEndpoints(endpoints =>
////{
////    endpoints.MapControllers();
////});

//app.UseSwagger();


////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller}/{action=Index}/{id?}");


////app.MapFallbackToFile("index.html"); ;

//app.MapControllers();
//app.Run();



