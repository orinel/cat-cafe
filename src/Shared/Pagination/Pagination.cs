namespace Pagination;

public class PageResult<T>(
    List<T> items,
    int pageNumber,
    int pageSize,
    int totalPages,
    int totalCount)
{
    public List<T> Items { get; set; } = items;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages { get; set; } = totalPages;
    public int TotalCount { get; set; } = totalCount;
}


public static class Paginator
{
    public static PageResult<T> Paginate<T>(
        List<T> items,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentException(
                "Page number must be greater than 0.",
                nameof(pageNumber));
        }

        if (pageSize < 1)
        {
            throw new ArgumentException(
                "Page size must be greater than 0.",
                nameof(pageSize));
        }

        var totalCount = items.Count;

        var totalPages = (int)Math.Ceiling(
            (double)totalCount / pageSize);

        var pageItems = items
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();

        return new PageResult<T>(
            pageItems,
            pageNumber,
            pageSize,
            totalPages,
            totalCount);
    }
}