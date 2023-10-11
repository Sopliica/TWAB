using Microsoft.AspNetCore.Mvc;
using TWAB.Api.Db;
using TWAB.Models.Models;

namespace TWAB.Api.Controllers;

public class UserController : Controller
{
    private readonly IMongoRepository<User> _userRepository;
    public UserController(IMongoRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("getAllUsers")]
    public List<User> getAllAsync()
    {
        return _userRepository.FilterBy(_ => true).ToList();
    }
    [HttpPost("registerUser")]
    public async Task AddProduct(User user)
    {
        await _userRepository.InsertOneAsync(user);
    }

    [HttpGet("getUserData")]
    public IEnumerable<string> GetProduct()
    {
        var users = _userRepository.FilterBy(
            filter => filter.Name != "test",
            projection => projection.Name
        );
        return users;
    }

    [HttpDelete("deleteUserData")]
    public void DeleteUser(string id)
    {
        _userRepository.DeleteByIdAsync(id);
    }

    [HttpPut("updateProductData")]
    public void UpdateUser(User user, string id)
    {
        var productToUpdate = _userRepository.FindById(id);
        user.Id = productToUpdate.Id;

        _userRepository.ReplaceOne(user);
    }
}
