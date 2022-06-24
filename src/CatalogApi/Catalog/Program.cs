using System.Net.Mime;
using System.Text.Json;
using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configure mongodb serializer
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

var mongoSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(serviceProvider => {
    return new MongoClient(mongoSettings.ToString());
});
builder.Services.AddSingleton<IItemRepository, MongoItemRepository>();

builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health check
builder.Services
    .AddHealthChecks()
    .AddMongoDb(
        mongoSettings.ToString(),
        name: "mongodb", 
        timeout: TimeSpan.FromSeconds(3),
        tags: new[]{"ready"});

// builder.Services
//     .AddHealthChecksUI(setup => setup.DisableDatabaseMigrations())
//     .AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health/ready", new HealthCheckOptions() {
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async (context, report) => {
        var result = JsonSerializer.Serialize(
            new {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    exception = e.Value.Exception?.Message ?? "none",
                    duration = e.Value.Duration.ToString()
                })
            }
        );
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions() {
    Predicate = (_) => false
});

//app.MapHealthChecksUI();

app.Run();
