using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Business.Services;
using DemoNLayerApi.CustomMiddleware;
using DemoNLayerApi.Data;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Data.Repository;
using DemoNLayerApi.Models.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger Gen
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter The Token Here"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// Connecting to DB
builder.Services.AddDbContext<AppDBContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

# region For JWT token
var jwtSettings = builder.Configuration.GetSection("JWTSettings");
//builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    // To handle exceptions
    options.Events = new JwtBearerEvents
    {
        OnChallenge = exc => throw new UnauthorizedAccessException("Token is missing"),
        OnForbidden = ctx =>
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            return ctx.Response.WriteAsJsonAsync(new
            {
                error = "You do not have enough privileges."
            });
        }
    };

});

#endregion

# region Role based authentication
builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AdminOnlyPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    }
    );
#endregion

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorServices, AuthorService>();
builder.Services.AddScoped<IUserRepository, UserRepositroy>();
builder.Services.AddScoped<IUserService, UserService>();
// For Authentication
builder.Services.AddScoped<UserSession>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

// For authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserSessionMiddleware>();

app.MapControllers();

app.Run();
