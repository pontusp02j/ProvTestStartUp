
using Core.Middleware;
using Core.Services;
using Core.Utilities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<DbContextService>(options => options.UseNpgsql(connectionString));
builder.Services.AddHttpContextAccessor();

builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Program).Assembly));


builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();


//Mappers to profile folder
builder.Services.AddAutoMapper(typeof(ProductProfile));

// Enable sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE");
        });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSession();
app.UseRouting();
app.UseCors();

app.UseFastEndpoints();

app.Run();