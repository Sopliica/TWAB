using MediatR;
using TWAB.Models.Models;

namespace TWAB.Api.Queries;

public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
