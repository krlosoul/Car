using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spa.Models;

namespace Spa.ApiController
{
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorController : ControllerBase
    {
        [Route("Error")]
        public ErrorModel Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        
            string message = context.Error.Message;
            
            ErrorModel.errorType type = ErrorModel.errorType.error;

            if (context.Error is CustomException)
                type = ErrorModel.errorType.warning; 

            Response.StatusCode = ((int)type);

            return new ErrorModel(message, type);
        }
    }
}
