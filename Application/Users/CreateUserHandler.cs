using OmniWorks.Domain.Users;
using OmniWorks.SharedKernal;

namespace OmniWorks.Application.Users;

public record CreateUserCommand(string action)
{
}

public class CreateUserHandler
{
    public async Task<Result<Guid>> Handle(CreateUserCommand command)
    {
        return command.action switch
        {
            "validation" => Result.Failure<Guid>(UserErrors.EmailNotUnique),
            "error" => throw new Exception("there was an error!"),
            _ => Guid.NewGuid()
        };
    }
}