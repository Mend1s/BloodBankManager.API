using BloodBankManager.Application.Services;
using BloodBankManager.Core.Services;
using BloodBankManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BloodManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloodManagement")));

builder.Services.AddScoped<IBloodStorageService, BloodStorageService>();

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
