using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetPostsHandler : IRequestHandler<GetPosts, PaginationModel<BlogPostDto>>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    #endregion

    #region CTORS :
    public GetPostsHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<PaginationModel<BlogPostDto>> Handle(GetPosts request, CancellationToken cancellationToken)
    {
        var query = _posts.AsNoTracking();
        if (request.CategoryId != default && request.CategoryId != Guid.Empty)
            query = query.Where(x => x.CategoryId.Equals(request.CategoryId));
        if (string.IsNullOrWhiteSpace(request.Search) is false)
            query = query.Where(x => (x.Title + x.Description + x.Category.Name).ToLower().Contains(request.Search.ToLower()));

        var mappedQuery = query.Select(x => new BlogPostDto
        {
            Id = x.Id,
            Category = x.Category.Name,
            Title = x.Title,
            Description = x.Description,
            PostDate = x.Created.ToString("dd MMMMM yyyy"),
            CommentsCount = x.Comments.Count,
            Image = x.Image,
            TimeAgo = TimeAgoHelper.Create(x.Created),
            User = new UserDto
            {
                Id = x.User.Id,
                FullName = String.Join(" ", x.User.FirstName, x.User.LastName),
                Image = x.User.Image,
            },
        });

        return await new PaginationHelper<BlogPostDto>(request.PageNumber, request.PageSize, mappedQuery).CreateAsync(cancellationToken);
    }
    #endregion
}
