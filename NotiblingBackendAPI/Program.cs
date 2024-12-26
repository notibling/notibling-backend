using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotiblingBackend.Application.Interfaces.UseCases.User;
using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Application.UseCases.User;
using NotiblingBackend.Application.UseCases.User.Company;
using NotiblingBackend.DataAccess;
using NotiblingBackend.DataAccess.Repositories;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
// Cargar claves RSA desde archivos
var privateKey = RSA.Create();
privateKey.ImportFromPem(File.ReadAllText("Keys/rsa-private-key.pem"));

var publicKey = RSA.Create();
publicKey.ImportFromPem(File.ReadAllText("Keys/rsa-public-key.pem"));

// Agregar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            IssuerSigningKey = new RsaSecurityKey(publicKey) // Validación con clave pública
        };
    });
*/

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Cargar claves desde los archivos
var privateKey = RSA.Create();
privateKey.ImportFromPem(File.ReadAllText(jwtSettings["PrivateKeyPath"]));
var publicKey = RSA.Create();
publicKey.ImportFromPem(File.ReadAllText(jwtSettings["PublicKeyPath"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new RsaSecurityKey(publicKey) // Usar la clave pública
    };
});

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

#region User
builder.Services.AddScoped<IAuthentication, Authentication>();
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

app.UseAuthentication(); // Middleware de autenticación
app.UseAuthorization();

app.MapControllers();

app.Run();
