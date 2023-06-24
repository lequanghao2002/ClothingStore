using ClothingStore.Data;
using ClothingStore.Repositories.Categories;
using ClothingStore.Repositories.Products;
using ClothingStore.Repositories.Orders;
using ClothingStore.Repositories.Authorize;
using ClothingStore.Repositories.Manage_Images;
using ClothingStore.Repositories.Users;
using ClothingStore.Repositories.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Clothing Store API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddHttpContextAccessor();

// Register DB
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddDbContext<Clothing_Store_AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClothingStoreAuthConnection")!);
});

// Register Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IManage_ImageRepository, Manage_ImageRepository>();
builder.Services.AddScoped<IAuthoritiesRepository, AuthorizeRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>(); 

// config idenity user
builder.Services.AddIdentityCore<IdentityUser>() 
    .AddRoles<IdentityRole>() 
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CLothingStore_Auth") 
    .AddEntityFrameworkStores<Clothing_Store_AuthDbContext>() 
    .AddDefaultTokenProviders(); 
builder.Services.Configure<IdentityOptions>(option => {
    option.Password.RequireDigit = false; 
    option.Password.RequireLowercase = false; 
    option.Password.RequireNonAlphanumeric = false; 
    option.Password.RequireUppercase = false; 
    option.Password.RequiredLength = 6; 
    option.Password.RequiredUniqueChars = 1; });

// register service authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
            ))
    };
});

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
