using Bogus;
using BogusDataCreate.Models;
using Microsoft.EntityFrameworkCore;

// 1. Create the database (if not exists)
using (var db = new AppDbContext())
{
    db.Database.EnsureCreated();
    Console.WriteLine("Database ready.");
}
// 2. Generate fake products
var productFaker = new Faker<Product>()
    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
    .RuleFor(p => p.Price, f => f.Finance.Amount(10, 1000))
    .RuleFor(p => p.Category, f => f.Commerce.Department());
var fakeProducts = productFaker.Generate(50);
// 3. Insert into the database
using (var db = new AppDbContext())
{
    // Only insert if the table is empty (so re-running doesn't duplicate)
    if (!db.Products.Any())
    {
        db.Products.AddRange(fakeProducts);
        db.SaveChanges();
        Console.WriteLine($"Inserted {fakeProducts.Count} products.");
    }
    else
    {
        Console.WriteLine("Products already exist. Skipping insert.");
    }
}
// 4. Read back and display
using (var db = new AppDbContext())
{
    var products = db.Products.OrderBy(p => p.Id).Take(10).ToList();
    Console.WriteLine("\n--- First 10 products in BogusDemoDb.Products ---");
    Console.WriteLine($"{"Id",-5} {"Name",-40} {"Price",10} {"Category",-25}");
    Console.WriteLine(new string('-', 85));
    foreach (var p in products)
    {
        Console.WriteLine($"{p.Id,-5} {p.Name,-40} {p.Price,10:F2} {p.Category,-25}");
    }
    var total = db.Products.Count();
    Console.WriteLine($"\nTotal products in table: {total}");
}