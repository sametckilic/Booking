using Booking.Persistence.MongoDbConfigurations.Models;
using Booking.Persistence.Extensions;
using System.Text.Json.Serialization;
using Booking.WebAPI.Extensions;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

// Add services to the container.
builder.WebHost.UseSentry(options =>
{
    options.ConfigureScope(scope =>
    {
        scope.Level = SentryLevel.Error;
    });
});

builder.Services.AddLogging();

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Sentry(o =>
    {
        o.Dsn = builder.Configuration["Sentry:Dsn"];
        o.MinimumBreadcrumbLevel = Serilog.Events.LogEventLevel.Error;
        o.MinimumEventLevel = Serilog.Events.LogEventLevel.Error;
    }).CreateLogger();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastrucutreRegistration(builder.Configuration);

var app = builder.Build();

app.UseSentryTracing();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
