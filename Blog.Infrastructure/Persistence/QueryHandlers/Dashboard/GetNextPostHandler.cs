using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetNextPostHandler : IRequestHandler<GetNextPost, NextPrevPostDto>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetNextPostHandler(ReadDbContext context, IMapper mapper)
    {
        _posts = context.Posts;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<NextPrevPostDto> Handle(GetNextPost request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking()
            .Where(x => x.UserId.Equals(request.UserId))
            .SkipWhile(x => !x.Id.Equals(request.PostId))
            .Skip(1)
            .ProjectTo<NextPrevPostDto>(_configurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
    #endregion
}
