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


var builder = WebApplication.CreateBuilder(args);


// add database service
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
dataSourceBuilder.MapEnum<Role>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
}
);



// add auto-mapper service
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// add DI services
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

// Add JWT Authentication
// by default cookie
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

// Add Authorization - later 
builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    }
    );

// add controllers
builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// after database
var app = builder.Build();

// Test database connection
// after app
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    try
    {
        // Check if the application can connect to the database
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database is connected");
        }
        else
        {
            Console.WriteLine("Unable to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}



app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandler>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();

