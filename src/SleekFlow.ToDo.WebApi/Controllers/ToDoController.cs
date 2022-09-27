using Microsoft.AspNetCore.Mvc;

namespace SleekFlow.ToDo.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ILogger<ToDoController> _logger;

    public ToDoController(ILogger<ToDoController> logger)
    {
        this._logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return this.Ok(new { Message = "Hello" });
    }
}