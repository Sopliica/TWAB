using MediatR;
using TWAB.Api.Commands;
using TWAB.Api.Db;
using TWAB.Models.Models;

namespace TWAB.Api.Handlers;

public class AddProductHandler : IRequestHandler<AddProductCommand>
{
    private readonly IMongoRepository<Product> _productRepository;

    public AddProductHandler(IMongoRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    

    public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.InsertOneAsync(request.Product);
    }
}