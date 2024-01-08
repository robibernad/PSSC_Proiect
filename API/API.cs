using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProiectPsscDb>(opt =>
{
    opt.UseSqlServer("Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True");
    //opt.UseSqlServer("Server=localhost\\MSSQLSERVER05;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True");
});

using (var dbContext = new ProiectPsscDb())
{
    dbContext.Database.Migrate();
}

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
