using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.WebAPI.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/products", async ([FromServices] IProductService productService, [FromQuery] string search="", [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null ) =>
            {
                ProductSearchDto searchDto = new ProductSearchDto() 
                { 
                    MaxPrice = maxPrice,
                    MinPrice = minPrice, 
                    Search = search 
                };

                var products = await productService.GetAllAsync(searchDto);
                return Results.Ok(products);
            })
            .WithName("GetAllProducts")
            .Produces<List<ProductDto>>(StatusCodes.Status200OK);

            routes.MapGet("/products/{id}", async ([FromServices] IProductService productService, Guid id) =>
            {
                try
                {
                    var product = await productService.GetByIdAsync(id);
                    return Results.Ok(product);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { Message = "Product not found." });
                }
            })
            .WithName("GetProductById")
            .DisableAntiforgery() 
                .Produces<ProductDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapPost("/products", async ([FromServices] IProductService productService, [FromBody] ProductCreateDto productDto) =>
            {
                
                await productService.AddAsync(productDto);
                return Results.Created($"/products/", productDto);
            })
            .WithName("CreateProduct")
            .Produces<ProductCreateDto>(StatusCodes.Status201Created);

            routes.MapPut("/products", async ([FromServices] IProductService productService, [FromBody] ProductUpdateDto productDto) =>
            {
                try
                {
                    await productService.UpdateAsync(productDto);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { Message = "Product not found." });
                }
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapDelete("/products/{id}", async ([FromServices] IProductService productService, Guid id) =>
            {
                try
                {
                    await productService.DeleteAsync(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { Message = "Product not found." });
                }
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }

}
