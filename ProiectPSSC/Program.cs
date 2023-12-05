
using (var dbContext = new ProiectPsscDb())
{
    dbContext.Database.Migrate();
}