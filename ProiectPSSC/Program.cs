using Data;
using Microsoft.EntityFrameworkCore;

string ConnectionString = "Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True";


var dbContextBuilder = new DbContextOptionsBuilder<ProiectPsscDb>()
                                                .UseSqlServer(ConnectionString).Options;

using (var dbContext = new ProiectPsscDb(dbContextBuilder))
{
    dbContext.Database.Migrate();
}
