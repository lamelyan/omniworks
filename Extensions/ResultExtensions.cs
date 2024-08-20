using OmniWorks.SharedKernal;

namespace OmniWorks.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert success result to problem");
        }

        return Results.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: result.Error.Code,
            detail: result.Error.Description
        );
    }
}