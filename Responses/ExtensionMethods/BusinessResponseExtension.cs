using Microsoft.AspNetCore.Mvc;

namespace BaseUtility
{
    public static class BusinessResponseExtension
    {
        public static IActionResult ToActionResult<TData>(this BusinessResponse<TData> response)
            where TData : class
        {
            return response.StatusCode switch
            {
                BusinessCode.Ok => new OkObjectResult(response),
                BusinessCode.Created => new CreatedResult(string.Empty, response),
                BusinessCode.BadRequest => new BadRequestObjectResult(response),
                BusinessCode.Unauthorized => new UnauthorizedResult(),
                BusinessCode.Forbidden => new ForbidResult(),
                BusinessCode.NotFound => new NotFoundObjectResult(response),
                BusinessCode.InternalServerError => new ObjectResult(response) { StatusCode = 500 },
                _ => new ObjectResult(response) { StatusCode = 500 }
            };
        }

        public static IActionResult ToActionResult<TInputData, TOutputData>(this BusinessResponse<TInputData> response, IMapper<TInputData, TOutputData> mapper)
            where TInputData : class
            where TOutputData : class
        {
            if(response.Data is IEnumerable<TInputData> inputEnumerable)
            {
                var newCollectionRes = new BusinessResponse<IEnumerable<TOutputData>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                    Data = inputEnumerable.Select(mapper.Map)
                };
                return newCollectionRes.ToActionResult();
            }
            var newResponse = new BusinessResponse<TOutputData>
            {
                StatusCode = response.StatusCode,
                Message = response.Message,
                Data = response.Data is null ? null : mapper.Map(response.Data)
            };
            return newResponse.ToActionResult();
        }
    }
}
