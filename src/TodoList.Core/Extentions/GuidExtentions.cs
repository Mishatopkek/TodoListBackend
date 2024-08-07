namespace TodoList.Core.Extentions;

public static class GuidExtentions
{
    public static Ulid ToUlid(this Guid guid)
    {
        return new Ulid(guid);
    }
}
