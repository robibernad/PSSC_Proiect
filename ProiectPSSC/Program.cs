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

Client client = new Client("asd238as", "as238dad", 073243223423, "Strada Bernabeu");
List<UnvalidatedProduct> products = new List<UnvalidatedProduct>() { new UnvalidatedProduct { Id = "3", Quantity = 1} };
AddOrderWorkFlow workflow = new AddOrderWorkFlow(new ProiectPsscDb(dbContextBuilder));
workflow.execute(client, products);
var orderIdToDelete = "a0e15c6d-af3e-48a5-be72-a79002aa77fc";
RemoveWorkflow removeWorkflow = new RemoveWorkflow(new ProiectPsscDb(dbContextBuilder));
var removeResult = removeWorkflow.Execute(orderIdToDelete).Result; 
Console.WriteLine("Done");
Console.ReadKey();