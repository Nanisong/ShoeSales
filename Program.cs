using Microsoft.EntityFrameworkCore;
using ShoeSales.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
{
    var uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
    return new MongoClient(uri);
});

builder.Services.AddControllers();
////Add versioning API
builder.Services.AddApiVersioning(options =>
{
    //true: API can return version in HEADER
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;

    //Option3: QueryString not safe
    //options.ApiVersionReader = new QueryStringApiVersionReader("SMTAFE-api-Version");

    // @@@ Safest Option: HTTP Header @@@
    options.ApiVersionReader = new HeaderApiVersionReader("X-HARRYPOTTER-version");
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// *** With this swagger has Error when is has two versions ("Failed to load API definition")
//builder.Services.AddEndpointsApiExplorer();
// *** To solve swagger Error
builder.Services.AddVersionedApiExplorer(options =>
{
    //This way says we have V and then the version number
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen();
// Comment out for mongo
//builder.Services.AddDbContext<ShopContext>(options =>
//{
//    options.UseInMemoryDatabase("Shop");
//});
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder
        .WithOrigins("https://localhost:7117")
        .WithHeaders("X-HARRYPOTTER-Version");
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
