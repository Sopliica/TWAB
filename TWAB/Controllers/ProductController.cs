using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TWAB.Api.Db;
using TWAB.Api.Queries;
using TWAB.Models.Models;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMongoRepository<Product> _productRepository;
    private readonly IMediator _mediator;

    public ProductController(IMongoRepository<Product> peopleRepository, IMediator mediator)
    {
        _productRepository = peopleRepository;
        _mediator = mediator;
    }

    [HttpGet("getAllProducts")]
    public List<Product> getAllAsync()
    {
        return _productRepository.FilterBy(_ => true).ToList(); 
    }
    [HttpPost("registerProduct")]
    public async Task AddProduct(Product product)
    {
        await _productRepository.InsertOneAsync(product);
    }

    [HttpGet("getProductData")]
    public IEnumerable<string> GetProduct()
    {
        var people = _productRepository.FilterBy(
            filter => filter.Name != "test",
            projection => projection.Name
        );
        return people;
    }

    [HttpDelete("deleteProductData")]
    public void DeletePerson(string id)
    {
        _productRepository.DeleteByIdAsync(id);
    }

    [HttpPut("updateProductData")]
    public void UpdateProduct(Product product, string id)
    {
        var productToUpdate = _productRepository.FindById(id);
        product.Id = productToUpdate.Id;

        _productRepository.ReplaceOne(product);
    }
    //////////////cqrs/////////////
    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
}