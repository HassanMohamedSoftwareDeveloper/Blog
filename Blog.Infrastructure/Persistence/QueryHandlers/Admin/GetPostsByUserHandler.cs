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
    #endregion

    #region CTORS :
    public GetPostsByUserHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<PaginationModel<UserPostDto>> Handle(GetPostsByUser request, CancellationToken cancellationToken)
    {
        var query = _posts.AsNoTracking().Where(x => x.UserId.Equals(request.UserId))
            .Select(x => new UserPostDto { Id = x.Id, Title = x.Title, Category = x.Category.Name });

        return await new PaginationHelper<UserPostDto>(request.PageNumber, request.PageNumber, query).CreateAsync(cancellationToken);
    }
    #endregion
}
