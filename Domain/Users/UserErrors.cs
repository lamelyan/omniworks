using OmniWorks.SharedKernal;

namespace OmniWorks.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => new("Users.NotFound", "The user with the id = {userId} was not found");

    public static readonly Error EmailNotUnique = new(
        "Users.EmailNotUnique", "The provided email is not unique");
}