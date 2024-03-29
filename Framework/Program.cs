using Aurora.Framework;
using Aurora.Framework.Applications;
using Aurora.Framework.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

Instance instance = new(1, "test");
InstanceContext context = new(instance);
IdentifierApplication application = new(context);

List<long> ids = new List<long>();
for (int i = 0; i < 10000000; i++)
{
    ids.Add(application.Get());
}
Console.WriteLine(ids.ToHashSet<long>().Count);

app.Run();
