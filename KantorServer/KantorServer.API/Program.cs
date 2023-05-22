using KantorServer.Application.Services;
using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connString);

});
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<ITransactionService,TransactionService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IUserService, UserService>();


//builder.Services.AddCertificateForwarding(options =>
//{
//    options.CertificateHeader = "ssl-client-cert";

//    options.HeaderConverter = (headerValue) =>
//    {
//        X509Certificate2? clientCertificate = null;

//        if (!string.IsNullOrWhiteSpace(headerValue))
//        {
//            clientCertificate = X509Certificate2.CreateFromPem(
//                WebUtility.UrlDecode(headerValue));
//        }

//        return clientCertificate!;
//    };
//});
var app = builder.Build();
await using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
dbContext.Database.Migrate();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
