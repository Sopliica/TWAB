using MediatR;
using TWAB.Models.Models;

namespace TWAB.Api.Commands;

public record DeleteProductCommand(string id) : IRequest;
