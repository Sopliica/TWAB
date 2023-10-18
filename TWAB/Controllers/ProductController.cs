using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TWAB.Api.Commands;
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

   /* [HttpGet("getAllProducts")]
    public List<Product> getAllAsync()
    {
        return _productRepository.FilterBy(_ => true).ToList();
    }
    /*[HttpPost("registerProduct")]
    public async Task AddProduct(Product product)
    {
        await _productRepository.InsertOneAsync(product);
    }*/

    [HttpGet("getProductData")]
    public IEnumerable<string> GetProduct()
    {
        var people = _productRepository.FilterBy(
            filter => filter.Name != "test",
            projection => projection.Name
        );
        return people;
    }

   /* [HttpDelete("deleteProductData")]
    public void DeleteProduct(string id)
    {
        _productRepository.DeleteByIdAsync(id);
    }*/

    /*[HttpPut("updateProductData")]
    public void UpdateProduct(Product product, string id)
    {
        var productToUpdate = _productRepository.FindById(id);
        product.Id = productToUpdate.Id;

        _productRepository.ReplaceOne(product);
    }*/
    //////////////cqrs/////////////
    [HttpGet("GetAllProducts")]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
    [HttpPost("AddProduct")]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        {
            await _mediator.Send(new AddProductCommand(product));
            return StatusCode(201);
        }
    }
    [HttpDelete("deleteProductData")]
    public async Task<ActionResult> DeleteProduct(string id)
    {
        await _mediator.Send(new DeleteProductCommand(id)) ;
        return StatusCode(200);
    }
    [HttpPut("updateProductData")]
    public async Task<ActionResult> UpdateProduct(Product product, string id)
    {
        await _mediator.Send(new UpdateProductCommand(product, id));
        return StatusCode(204); 
    }
}