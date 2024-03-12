using BloodBankManager.Application.Services;
using BloodBankManager.Core.Services;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers()
//                .AddJsonOptions(options => options.JsonSerializerOptions
//                .Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBloodStorageService, BloodStorageService>();

builder.Services.AddDbContext<BloodManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloodManagement")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
