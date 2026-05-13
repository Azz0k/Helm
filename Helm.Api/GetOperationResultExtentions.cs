using Helm.Core.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Helm.Api
{
    public static class GetOperationResultExtentions
    {
        public static IActionResult ToHttp<T>(this GetOperationResult<T> result, SuccessCodes code)
        {
            return result switch
            {
                GetOperationResult<T>.Success x => GetSuccessResult(x, code),
                GetOperationResult<T>.NotFound => new NotFoundResult(),
                GetOperationResult<T>.Invalid => new BadRequestResult(),
                GetOperationResult<T>.Forbidden => new ForbidResult(),
                GetOperationResult<T>.Conflict => new ConflictResult(),
                _ => new StatusCodeResult(500)
            };
        }
        private static IActionResult GetSuccessResult<T>(GetOperationResult<T>.Success x, SuccessCodes code)
        {
            return code switch
            {
                SuccessCodes.Ok => new OkObjectResult(x.Data),
                SuccessCodes.Created => new CreatedResult("", x.Data),
                SuccessCodes.NoContent => new NoContentResult(),
                _ => new StatusCodeResult(500)
            };
        }

    }
}
