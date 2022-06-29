using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.DTOS.Admin;
using Blog.Application.Pagination;
using Blog.Application.Queries.Admin;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Admin;

internal sealed class GetPostsByUserHandler : IRequestHandler<GetPostsByUser, PaginationModel<UserPostDto>>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetPostsByUserHandler(ReadDbContext context, IMapper mapper)
    {
        _posts = context.Posts;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<PaginationModel<UserPostDto>> Handle(GetPostsByUser request, CancellationToken cancellationToken)
    {
        var query = _posts.AsNoTracking().Where(x => x.UserId.Equals(request.UserId));
        if (string.IsNullOrWhiteSpace(request.Search) is false)
            query = query.Where(x => x.Title.Contains(request.Search) || x.Category.Name.Contains(request.Search));
        var sourceQuery = query.OrderBy(x => x.Created).ProjectTo<UserPostDto>(_configurationProvider);

        return await new PaginationHelper<UserPostDto>(request.PageNumber, request.PageSize, sourceQuery).CreateAsync(cancellationToken);
    }
    #endregion
}
