using System.Text;
using FormWise.WebApi.Configuration;
using FormWise.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Framework-level services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();


// 2. Infrastructure / Application services
builder.Services.AddCustomDbContext(builder.Configuration);

builder.Services.AddCoreServices();

builder.Services.AddConfigurationOptions(builder.Configuration);

var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

builder.Services.AddCustomAuthentication(key);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

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
