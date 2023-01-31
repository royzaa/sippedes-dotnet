using Microsoft.AspNetCore.Mvc;

namespace livecode_net_advanced.Cores.Dto;

public class TestActionResult : IActionResult
{
    private readonly CommonResponse<object> _result;

    public TestActionResult(CommonResponse<object> result)
    {
        _result = result;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new JsonResult(_result.Data)
        {
            ContentType = null,
            SerializerSettings = null,
            StatusCode = null
        };
        
        

        await objectResult.ExecuteResultAsync(context);
    }
}