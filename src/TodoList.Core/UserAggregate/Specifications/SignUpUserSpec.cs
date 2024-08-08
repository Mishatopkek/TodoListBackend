using Ardalis.Specification;

namespace TodoList.Core.UserAggregate.Specifications;

public class SignUpUserSpec : Specification<User>
{
    public SignUpUserSpec(string name, string email)
    {
        Query.Where(user =>
            user.Name == name ||
            user.Email == email
        );
    }
}
