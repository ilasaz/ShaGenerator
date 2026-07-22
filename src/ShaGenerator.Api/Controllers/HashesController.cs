using Microsoft.AspNetCore.Mvc;
using ShaGenerator.Application.Hashes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShaGenerator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class HashesController : ControllerBase
{
    private readonly IHashPublisher _publisher;

    public HashesController(IHashPublisher publisher) => _publisher = publisher;

    [HttpPost]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        string message = "Testing message";
        await _publisher.PublishAsync(message, cancellationToken);
        return Accepted();
    }
}
