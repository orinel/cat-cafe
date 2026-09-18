namespace Shared.Filtering;

public static class Filtering
{
    public static List<T> Filter<T>(
        List<T> items,
        Func<T, bool> predicate)
    {
        return items.Where(predicate).ToList();
    }
}