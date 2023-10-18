using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using TWAB.Api.Db;
using TWAB.Models.Models;

namespace TWAB.Api.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private readonly IMongoRepository<Product> _productRepository;
    public ProductService()
    {
        _httpClient = new HttpClient();
    }
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        List<Product> result = new();
        var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");
        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        foreach (var p in products)
        {
            Product productToAdd = new Product()
            {
                Name = p.Title,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
                Image = p.Image,
                Rating = new Rating
                {
                    Rate = p.Rating.Rate,
                    Count = p.Rating.Count
                }
            };
            result.Add(productToAdd);
        }
        return result;  
    }
}
