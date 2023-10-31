using FinancialControl.Core.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinancialControl.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public ResponseDto<object> Error()
    {
        ResponseDto<object> response = new ResponseDto<object>();
        Response.StatusCode = 500;
        var id = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;

        response.Success = false;
        response.Data = null;
        response.Erros.Add(id);

        return response;
    }
}
