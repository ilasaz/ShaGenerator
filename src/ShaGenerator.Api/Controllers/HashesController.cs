using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;
using ShaGenerator.Application.Hashes.Get;
using ShaGenerator.Application.Services;
using ShaGenerator.Contracts.Hashes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShaGenerator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class HashesController : ControllerBase
{
    private readonly HashGeneratorService _generator;
    private readonly IQueryHandler<GetHashesQuery, HashesByDateResponse> _handler;

    public HashesController(
        HashGeneratorService generator, 
        IQueryHandler<GetHashesQuery, HashesByDateResponse> handler)
    {
        _generator = generator;
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        int published = await _generator.GenerateAndPublishAsync(cancellationToken);
        return Accepted(new { published });
    }


    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetHashesQuery();
        HashesByDateResponse result = await _handler.Handle(query, cancellationToken);

        return Ok(result);
    }
}
