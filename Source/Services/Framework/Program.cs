using Aurora.Framework.Applications;
using Aurora.Framework.Applications.Extensions;
using Aurora.Framework.Data;
using Aurora.Framework.Data.Extensions;
using Aurora.Library.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddDataService();
builder.Services.AddApplications();
builder.Services.AddSingleton<InstanceContext>();

DatabaseContext context = new();
await context.Database.EnsureDeletedAsync();

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
