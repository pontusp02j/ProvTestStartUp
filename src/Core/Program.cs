
using Core.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<DbContextService>(options => options.UseNpgsql(connectionString));
builder.Services.AddHttpContextAccessor();

// builder.Services.AddTransient<ICarRepository, CarRepository>();

builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Program).Assembly));


//Mappers to profile folder
//builder.Services.AddAutoMapper(typeof(CarProfile));

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
            policy.WithOrigins("https://localhost:44420")
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE");
        });
});

var app = builder.Build();

/* incase we need to migrate any default value
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DbContextService>();
        dbContext.Database.Migrate();
        await InitialSetup.InsertInitialDbData(dbContext);
    } 
*/


app.UseSession();
app.UseRouting();
app.UseCors();

app.UseFastEndpoints();

app.Run();