using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Application.UseCases.User.Company;
using NotiblingBackend.DataAccess;
using NotiblingBackend.DataAccess.Repositories;
using NotiblingBackend.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverterWithTimeZone());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Data base connection
builder.Services.AddDbContext<NBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("BDConnection")));
#endregion

#region Dependency Injection Services

//Repositories
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Use cases
#region Company
builder.Services.AddScoped<IAddCompany, AddCompany>();
builder.Services.AddScoped<IGetByIdCompany, GetByIdCompany>();
builder.Services.AddScoped<IGetAllCompany, GetAllCompany>();
builder.Services.AddScoped<IUpdateCompany, UpdateCompany>();
builder.Services.AddScoped<ISoftDeleteCompany, SoftDeleteCompany>();
#endregion

#endregion

var app = builder.Build();

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
