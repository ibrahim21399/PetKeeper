using Application;
using Application.Interfaces.Repos.Auth;
using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Repos.General;
using Application.Interfaces.Repositories.General;
using Application.Interfaces.Services.Auth;
using Application.Interfaces.Services.BusinessOwner;
using Application.Interfaces.Services.General;
using Application.Services.Auth;
using Application.Services.BusinussOwner;
using Application.Services.General;
using Domain.Entites;
using Domain.Entites.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories.General;
using Presistence;
using Presistence.Interfaces.Repos;
using Presistence.Repos.Auth;
using Presistence.Repos.BusinessOwner;
using Presistence.Repos.General;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


#region IdentityCoreAuth
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnStr"));
    options.EnableSensitiveDataLogging();

});
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});
#endregion


#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = "PetKeeper.com",
        ValidAudience = "PetKeeper.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretPassword"))
    };
});
#endregion

builder.Services.AddAutoMapper(typeof(MappingProfileBase));
//builder.Services.AddCors(c =>
//{
//    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
//});



#region Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ICityAreaRepository<Area>, CityAreaRepository<Area>>();
builder.Services.AddScoped<ICityAreaRepository<City>, CityAreaRepository<City>>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IBusinessRepository,BusinessRepository>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();




#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICitiesAreasService,CitiesAreasService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<ICreateBusinessService, CreateBusinessService>();

#endregion



//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnStr")));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
