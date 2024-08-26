namespace TodoList.Core.Extensions;

public static class GuidExtensions
{
    public static Ulid ToUlid(this Guid guid)
    {
        return new Ulid(guid);
    }
}
