using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using user.src.Database;
using user.src.Middleware;
using user.src.Repository;
using user.src.Services;
using user.src.Services.user;
using user.src.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using user.src.Services.product;
using user.src.Entity;
using user.src.Services.orderDetail;
using user.src.Services.order;
using System.Text.Json.Serialization;
using System.Net;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with a scoped lifetime
// builder.Services.AddDbContext<YourDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("Local")));

// add database service
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
dataSourceBuilder.MapEnum<Role>();

var dataSource = dataSourceBuilder.Build();
builder.Services.AddSingleton(dataSource);

// services.AddDbContext<DatabaseContext>(options =>
// {
//     options.UseNpgsql(_config.GetConnectionString("Local"));
// });

// builder.Services.AddDbContext<DatabaseContext>(options =>
// {
//     options.UseNpgsql(dataSourceBuilder.Build());
// }
// );
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSource);
});


builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services
    .AddScoped<ICategoryService, CategoryServices>()
    .AddScoped<CategoryRepo, CategoryRepo>()

    .AddScoped<IUserService, UserService>()
    .AddScoped<UserRepo, UserRepo>()

    .AddScoped<IProductService, ProductService>()
    .AddScoped<ProductRepository, ProductRepository>()

     .AddScoped<IOrderDetailService, OrderDetailService>()
    .AddScoped<OrderDetailRepository, OrderDetailRepository>()


    .AddScoped<IOrderService, OrderService>()
    .AddScoped<OrderRepository, OrderRepository>();

// cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
                          .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true)
                            .AllowCredentials();
                      });
});

// later when you deployed FE => add in line 77

// Add JWT Authentication, by default cookie
builder.Services
.AddAuthentication(options =>
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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    }
    );
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.OpenConnection();

    try
    {
        // Check if the application can connect to the database
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database is connected");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}


app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandler>();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello, World!");

app.Run();

