using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SailMapper.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Swagger Documentation Section
var info = new OpenApiInfo()
{
    Title = "Your API Documentation",
    Version = "v1",
    Description = "Description of your API",
    Contact = new OpenApiContact()
    {
        Name = "Your name",
        Email = "your@email.com",
    }

};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<SailDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConn"), new MySqlServerVersion(new Version(8, 0, 2))));

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger(u =>
        {
            u.RouteTemplate = "swagger/{documentName}/swagger.json";
        });

app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Your API Title or Version");
    });
//}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SailDBContext>();
    if (!context.Database.CanConnect())
    {
        //throw new Exception("cannot connect to database " + context.Database.GetConnectionString());
    }
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();