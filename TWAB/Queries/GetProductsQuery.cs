using MediatR;
using TWAB.Models.Models;

namespace TWAB.Api.Queries;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{

}
