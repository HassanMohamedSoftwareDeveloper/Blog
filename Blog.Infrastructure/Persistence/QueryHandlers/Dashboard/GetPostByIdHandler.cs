using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetPostByIdHandler : IRequestHandler<GetPostById, PostDto>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    #endregion

    #region CTORS :
    public GetPostByIdHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<PostDto> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking().Where(x => x.Id.Equals(request.Id))
            .Select(x => new PostDto
            {
                Id = x.Id,
                Category = x.Category.Name,
                Title = x.Title,
                Description = x.Description,
                PostDate = x.Created.ToString("dd MMMMM yyyy"),
                CommentsCount = x.Comments.Count,
                Image = x.Image,
                ViewsCount = x.ViewersCount,
                TimeAgo = TimeAgoHelper.Create(x.Created),
                User = new UserDto
                {
                    Id = x.User.Id,
                    FullName = String.Join(" ", x.User.FirstName, x.User.LastName),
                    Image = x.User.Image,
                },
                Comments = x.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Message = c.Message,
                    CommentDate = c.Created.ToString("dd MMMMM yyyy"),
                    User = new UserDto
                    {
                        Id = c.User.Id,
                        FullName = String.Join(" ", c.User.FirstName, c.User.LastName),
                        Image = c.User.Image,
                    },
                    Replies = c.Replies.Select(r => new ReplyDto
                    {
                        Id = r.Id,
                        Message = r.Message,
                        ReplyDate = r.Created.ToString("dd MMMMM yyyy"),
                        User = new UserDto
                        {
                            Id = c.User.Id,
                            FullName = String.Join(" ", c.User.FirstName, c.User.LastName),
                            Image = c.User.Image,
                        },
                    }).ToList(),
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }
    #endregion
}
