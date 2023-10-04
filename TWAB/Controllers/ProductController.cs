using Microsoft.AspNetCore.Mvc;
using TWAB.Api.Db;
using TWAB.Models.Models;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    private readonly IMongoRepository<Product> _productRepository;

    public SampleController(IMongoRepository<Product> peopleRepository)
    {
        _productRepository = peopleRepository;
    }

    [HttpPost("registerProduct")]
    public async Task AddPerson(Product product)
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

    /*[HttpDelete("deleteProductData")]
    public void DeletePerson()
    {
        _productRepository.DeleteOne();
    }*/

    [HttpPut("updateProductData")]
    public void UpdateProduct(Product product, string id)
    {

        _productRepository.ReplaceOne(product);
    }
    
}