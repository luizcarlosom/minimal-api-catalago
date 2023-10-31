using Microsoft.EntityFrameworkCore;
using MinimalAPICatalago.Context;
using MinimalAPICatalago.Models;

namespace MinimalAPICatalago.ApiEndpoints;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        app.MapPost("/products", async (Product product, AppDbContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Results.Created($"/products/{product.ProductId}", product);
        });

        app.MapGet("/products", async (AppDbContext db) =>
            await db.Products.ToListAsync()).WithTags("Products").RequireAuthorization();

        app.MapGet("/products/{id:int}", async (int id, AppDbContext db) =>
        {
            return await db.Products.FindAsync(id)
                   is Product product
                   ? Results.Ok(product)
                   : Results.NotFound();
        });

        app.MapPut("/products/{id:int}", async (int id, Product product, AppDbContext db) =>
        {
            if (product.ProductId != id) return Results.BadRequest();

            var productDb = await db.Products.FindAsync(id);

            if (productDb is null) return Results.NotFound();

            productDb.Name = product.Name;
            productDb.Description = product.Description;
            productDb.Price = product.Price;
            productDb.Stock = product.Stock;
            productDb.Image = product.Image;
            productDb.PurchaseDate = product.PurchaseDate;
            productDb.CategoryId = product.CategoryId;

            await db.SaveChangesAsync();
            return Results.Ok(productDb);
        });

        app.MapDelete("/products/{id:int}", async (int id, AppDbContext db) =>
        {
            var product = await db.Products.FindAsync(id);

            if (product is null) return Results.NotFound();

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
