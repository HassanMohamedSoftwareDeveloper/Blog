using AutoMapper;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class CheckUserLikePostHandler : IRequestHandler<CheckUserLikePost, bool>
{
    #region Fields :
    private readonly DbSet<LikeReadModel> _likes;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public CheckUserLikePostHandler(ReadDbContext context, IMapper mapper)
    {
        _likes = context.Likes;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(CheckUserLikePost request, CancellationToken cancellationToken)
    {
        return await _likes.AsNoTracking().Where(x => x.PostId.Equals(request.PostId) && x.UserId.Equals(request.UserId)).AnyAsync();
    }
    #endregion
}