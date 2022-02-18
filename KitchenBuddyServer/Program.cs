using KitchenBuddyServer.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to the database
builder.Services.AddDbContext<DatabaseContext>(option => {
    // Get the database information from appsettings
    string server = builder.Configuration.GetValue<string>("DatabaseInformation:Server");
    string port = builder.Configuration.GetValue<string>("DatabaseInformation:Port");
    string database = builder.Configuration.GetValue<string>("DatabaseInformation:Database");
    string username = builder.Configuration.GetValue<string>("DatabaseInformation:Username");
    string password = builder.Configuration.GetValue<string>("DatabaseInformation:Password");

    // Add the connection
    string connectionString = String.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};",
        server, port, database, username, password);
    option.UseNpgsql(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("myPolicy", policyBuilder =>
        policyBuilder.WithOrigins("localhost").AllowAnyOrigin().AllowAnyHeader());
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

app.UseCors("myPolicy");

app.Run();
