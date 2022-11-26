using KitchenBuddyServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to the database
builder.Services.AddDbContext<DatabaseContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
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
