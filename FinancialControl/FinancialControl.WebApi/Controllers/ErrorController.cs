using FinancialControl.Core.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinancialControl.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        var errorId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;

        var response = new ResponseDto<object>
        {
            Success = false,
            Data = null,
            Erros = new List<string> { "An error occurred. Please try again later." },
        };

        return StatusCode(500, response);
    }
}
