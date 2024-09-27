namespace TodoList.Core.Extensions;

public static class StringExtensions
{
    public static Ulid ToUlid(this string guidString) => Guid.Parse(guidString).ToUlid();
}
