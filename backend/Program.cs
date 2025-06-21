using System.Text.Json.Serialization;
using backend.Data;
using backend.Interfaces;
using backend.Repository;
using backend.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

    /*
        ! 100 concurrent users load test for 2 minutes using postman
        //* BEFORE REDIS
         Total requests sent
                    3,986
                    Requests/second
                    31.50
                    Avg. response time
                    1,844 ms
                    P90
                    6,994 ms
                    P95
                    15,065 ms
                    P99
                    15,261 ms
                    Error rate
                    3.54 %

        //* AFTER REDIS
                    Total requests sent
                    5,456
                    Requests/second
                    41.81
                    Avg. response time
                    152 ms
                    P90
                    234 ms
                    P95
                    526 ms
                    P99
                    1,919 ms
                    Error rate
                    0.00 %
    */
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "RedisInstance";
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Default", policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICartRepository, CartRepository>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IProductService, ProductsService>();
    builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Default");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program
{

}