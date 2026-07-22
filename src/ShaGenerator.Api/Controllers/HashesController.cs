using Microsoft.AspNetCore.Mvc;
using ShaGenerator.Application.Hashes;
using ShaGenerator.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShaGenerator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class HashesController : ControllerBase
{
    private readonly HashGeneratorService _generator;

    public HashesController(HashGeneratorService generator) => _generator = generator;

    [HttpPost]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        int published = await _generator.GenerateAndPublishAsync(cancellationToken);
        return Accepted(new { published });
    }
}
