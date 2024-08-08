using Ardalis.Specification;

namespace TodoList.Core.UserAggregate.Specifications;

public sealed class LoginUserSpec : Specification<User>
{
    public LoginUserSpec(string login)
    {
        Query.Where(user =>
            user.Email == login ||
            user.Name == login
        );
    }
}
