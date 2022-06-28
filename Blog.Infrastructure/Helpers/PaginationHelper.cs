using Blog.Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Helpers;

internal sealed class PaginationHelper<TModel>
{
    #region Fields :
    private readonly int _pageNumber;
    private readonly int _pageSize;
    private readonly IQueryable<TModel> _sourceQuery;
    #endregion

    #region CTORS :
    public PaginationHelper(int pageNumber, int pageSize, IQueryable<TModel> sourceQuery)
    {
        _pageNumber = pageNumber == 0 ? 1 : pageNumber;
        _pageSize = pageSize == 0 ? 5 : pageSize;
        _sourceQuery = sourceQuery;
    }
    #endregion

    #region Methods :
    public async Task<PaginationModel<TModel>> CreateAsync(CancellationToken cancellationToken)
    {
        var totalCount = await _sourceQuery.CountAsync(cancellationToken);
        int pagesCount = (int)Math.Ceiling((double)totalCount / _pageSize);

        int skipAmount = _pageSize * (_pageNumber - 1);

        int capacity = skipAmount + _pageSize;
        bool hasPreviousPage = _pageNumber > 1;
        bool hasNextPage = totalCount > capacity;

        var pageModel = new PaginationModel<TModel>
        {
            PageNumber = _pageNumber,
            HasPreviousPage = hasPreviousPage,
            HasNextPage = hasNextPage,
            PagesCount = pagesCount,
            TotalCount = totalCount,
            Pages = PageNumbers(_pageNumber, pagesCount).ToList(),
            Data = (await _sourceQuery.Skip(skipAmount).Take(_pageSize).ToListAsync(cancellationToken)) ?? new List<TModel>(),
        };
        return pageModel;
    }
    #endregion

    #region Helpers :
    private static IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
    {
        if (pageCount <= 5)
            for (int i = 1; i <= pageCount; i++)
                yield return i;
        else
        {
            int midPoint = pageNumber < 3 ? 3 :
            pageNumber > pageCount - 2 ? pageCount - 2 : pageNumber;

            int lowerBound = midPoint - 2;
            int upperBound = midPoint + 2;

            if (lowerBound != 1)
            {
                yield return 1;
                if (lowerBound - 1 > 1)
                    yield return -1;
            }

            for (int i = lowerBound; i <= upperBound; i++)
                yield return i;

            if (upperBound != pageCount)
            {
                if (pageCount - upperBound > 1)
                    yield return -1;
                yield return pageCount;
            }
        }
    }
    #endregion
}
