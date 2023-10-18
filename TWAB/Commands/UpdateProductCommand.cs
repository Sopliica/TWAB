using MediatR;
using TWAB.Models.Models;

namespace TWAB.Api.Commands;

public record UpdateProductCommand(Product product, string id) : IRequest;

