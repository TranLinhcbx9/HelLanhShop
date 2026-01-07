using HelLanhShop.API.Common.ApiResponses;
using HelLanhShop.API.Middlewares;
using HelLanhShop.Application.Authentications.Models;
using HelLanhShop.Application.Common.Authorizations;
using HelLanhShop.Application.Common.Enums;
using HelLanhShop.Application.Common.Mappings;
using HelLanhShop.Infrastructure;
using HelLanhShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// add middleware custom exception

builder.Services.AddDbContext<HelLanhDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();

//mapping profile
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Tạo Options cho kiểu JwtSettings
//Bind data từ config (appsettings) vào class
//Đăng ký vào DI để sau này inject bằng IOptions<JwtSettings>

// Bind JwtSettings vào Options pattern 1 lần duy nhất
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// Lấy object ngay lập tức để config Authentication (chỉ 1 lần)
var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>()!;

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = ApiResponse.Fail(
                    message: "Unauthorized",
                    errorType: ErrorType.Unauthorized,
                    errorCode: ErrorCode.UNAUTHORIZED
                );

                return context.Response.WriteAsJsonAsync(response);
            },

            OnForbidden = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = ApiResponse.Fail(
                    message: "Permission denied",
                    errorType: ErrorType.Forbidden,
                    errorCode: ErrorCode.FORBIDDEN
                );

                return context.Response.WriteAsJsonAsync(response);
            }
        };
    });
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in PermissionConstants.All)
    {
        options.AddPolicy(permission, policy =>
        {
            policy.RequireClaim("permission", permission);
        });
    }
});
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Your Token: {your_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

app.UseExceptionMiddleware();

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
