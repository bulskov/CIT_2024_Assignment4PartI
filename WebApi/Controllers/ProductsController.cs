using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]

public class ProductsController : ControllerBase
{
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductsController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }
        var model = CreateProductModel(product);

        return Ok(model);
    }

    [HttpGet("category/{id}")]
    public IActionResult GetProductByCategory(int id)
    {
        var product = _dataService.GetProductByCategory(id);

        if (product.Count == 0)
        {
            return NotFound(product);
        }

        return Ok(product);
    }

    [HttpGet]
    public IActionResult GetProductByName(string name)
    {
        var product = _dataService.GetProductByName(name);

        if (product.Count == 0)
        {
            return NotFound(product);
        }

        return Ok(product);
    }

    


    private ProductModel? CreateProductModel(ProductWithCategoryName? product)
    {
        if (product == null)
        {
            return null;
        }

        var model = product.Adapt<ProductModel>();
        model.Url = GetUrl(product.Id);

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id });
    }

}