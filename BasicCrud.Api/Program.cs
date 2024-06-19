using BasicCrud.DAL.DependencyInjection;
using BasicCrud.Core.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// layers
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddCore();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
