using BloodBankManager.API.Filters;
using BloodBankManager.Application.Services.Implementations;
using BloodBankManager.Application.Services.Interfaces;
using BloodBankManager.Application.Validators;
using BloodBankManager.Infrastructure.EmailConfig;
using BloodBankManager.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateDonorValidator>())
    .AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Banco de Sangue", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddScoped<IBloodStorageService, BloodStorageService>();
builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<BloodManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloodManagement")));

var app = builder.Build();

app.UseCors("MyPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
