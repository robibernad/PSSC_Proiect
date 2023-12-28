using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Workflow;

string ConnectionString = "Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True";


var dbContextBuilder = new DbContextOptionsBuilder<ProiectPsscDb>()
                                                .UseSqlServer(ConnectionString).Options;

using (var dbContext = new ProiectPsscDb(dbContextBuilder))
{
    dbContext.Database.Migrate();
}

Client client = new Client("Ursei", "Alexandru", 073243223423, "Strada Bernabeu");
List<UnvalidatedProduct> products = new List<UnvalidatedProduct>() { new UnvalidatedProduct { Id = "1", Quantity = 2} };
AddOrderWorkFlow workflow = new AddOrderWorkFlow(new ProiectPsscDb(dbContextBuilder));
workflow.execute(client, products);
Console.WriteLine("Done");
Console.ReadKey();