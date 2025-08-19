using AutoMapper;
using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Interfaces;
using ExpenseTracker.Api.Mappings;
using ExpenseTracker.Api.Repositories;
using ExpenseTracker.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register ExpenseDbContext
builder.Services.AddDbContext<ExpenseDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseDb"), sqlOption =>
{
    sqlOption.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
}));

builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddAutoMapper(typeof(ExpenseMappingProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExpenseDbContext>();
    var retries = 5;
    while(retries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch(Exception ex)
        {
            retries--;
            Console.WriteLine($"Failed to migrate to DB, Message :{ex.Message}, Retries :{retries}");
            Thread.Sleep(5000);
        }
    }
}

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
