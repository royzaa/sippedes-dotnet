using System.Text.Json;
using livecode_net_advanced.Cores.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace livecode_net_advanced.Cores.Controller;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    protected IActionResult MyResult <T> (CommonResponse<T> result)
    {
        var jsonResult = new JsonResult(result);
        return jsonResult;
    }

    protected IActionResult Success <T> (T result)
    {
        return MyResult(new CommonResponse<T>
        {
            StatusCode = 200,
            Message = "Success",
            Data = result
        });
    }
}