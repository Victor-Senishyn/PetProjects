using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services.Interaces;
using OfficeControlSystemApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccessCardService, AccessCardService>();
builder.Services.AddScoped<IVisitHistoryService, VisitHistoryService>();

//builder.Services.AddScoped<IScopedService, EmployeeService>();
//builder.Services.AddScoped<IScopedService, AccessCardService>();
//builder.Services.AddScoped<IScopedService, VisitHistoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfficeControlSystem"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();