using Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Workflow;

string ConnectionString = "Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True";
//string ConnectionString = "Server=localhost\\MSSQLSERVER05;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True";


var dbContextBuilder = new DbContextOptionsBuilder<ProiectPsscDb>()
                                                .UseSqlServer(ConnectionString).Options;

using (var dbContext = new ProiectPsscDb(dbContextBuilder))
{
    dbContext.Database.Migrate();
    Console.WriteLine("Migrated");
}

//Client client = new Client("2", "2", 073243223423, "Strada Bernabeu");
//List<UnvalidatedProduct> products = new List<UnvalidatedProduct>() { new UnvalidatedProduct { Id = "3", Quantity = 10 } };
//AddOrderWorkFlow workflow = new AddOrderWorkFlow(new ProiectPsscDb(dbContextBuilder));
//workflow.execute(client, products);

//var orderIdToDelete = "a0e15c6d-af3e-48a5-be72-a79002aa77fc";
//RemoveWorkflow removeWorkflow = new RemoveWorkflow(new ProiectPsscDb(dbContextBuilder));
//var removeResult = removeWorkflow.Execute(orderIdToDelete).Result;

//var orderIdToUpdate = "e217f4c6-11de-48e8-b890-7add07601bfb";
//ModifyWorkFlow modifyWorkflow = new ModifyWorkFlow(new ProiectPsscDb(dbContextBuilder));
//List<UnvalidatedProduct> updatedProducts = new List<UnvalidatedProduct>() { new UnvalidatedProduct { Id = "3", Quantity = 5 } };
//string newAddress = "Strada veche";
//var modifyResult = modifyWorkflow.Execute(orderIdToUpdate, updatedProducts, newAddress).Result;

//var generator = new CreateBillWorkFlow(new ProiectPsscDb(dbContextBuilder));

//generator.GeneratePdfForOrder("e24291f7-4cfa-459c-8a7f-d4e32dff8493");

Console.WriteLine("Done");
Console.ReadKey();