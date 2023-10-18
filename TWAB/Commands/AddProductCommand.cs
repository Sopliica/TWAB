using MediatR;
using TWAB.Models.Models;

namespace TWAB.Api.Commands;

public record AddProductCommand(Product Product) : IRequest;


