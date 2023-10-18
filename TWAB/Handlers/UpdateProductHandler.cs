using MediatR;
using TWAB.Api.Commands;
using TWAB.Api.Db;
using TWAB.Models.Models;

namespace TWAB.Api.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IMongoRepository<Product> _productRepository;
    public UpdateProductHandler(IMongoRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = _productRepository.FindById(request.id);
        request.product.Id = productToUpdate.Id;

        _productRepository.ReplaceOne(request.product);
    }
}
