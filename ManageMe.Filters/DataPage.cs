namespace ManageMe.Core;

public record DataPage<T>(int TotalCount, int Page, IEnumerable<T> Data);
