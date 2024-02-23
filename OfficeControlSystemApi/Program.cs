using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services.Interaces;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data.Repositorys;
using Microsoft.AspNetCore.Identity;
using OfficeControlSystemApi.Models.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddAuthorization();//
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccessCardService, AccessCardService>();
builder.Services.AddScoped<IVisitHistoryService, VisitHistoryService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAccessCardRepository, AccessCardRepository>();
builder.Services.AddScoped<IVisitHistoryRepository, VisitHistoryRepository>();

builder.Services.AddScoped<ICreateEmployeeCommand, CreateEmployeeCommand>();
builder.Services.AddScoped<ICreateVisitHistoryCommand, CreateVisitHistoryCommand>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfficeControlSystem"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapIdentityApi<IdentityUser>();//

app.Run();