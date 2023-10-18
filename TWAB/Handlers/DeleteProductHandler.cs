using MediatR;
using TWAB.Api.Commands;
using TWAB.Api.Db;
using TWAB.Api.Queries;
using TWAB.Models.Models;

namespace TWAB.Api.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IMongoRepository<Product> _productRepository;
    public DeleteProductHandler(IMongoRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteByIdAsync(request.id);
    }
}
