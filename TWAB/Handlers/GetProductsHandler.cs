using MediatR;
using TWAB.Api.Db;
using TWAB.Api.Queries;
using TWAB.Models.Models;

namespace TWAB.Api.Handlers;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IMongoRepository<Product> _productRepository;
    public GetProductsHandler(IMongoRepository<Product> productRepository) 
    { 
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await _productRepository.FilterByAsync(_ => true);
    }
}
