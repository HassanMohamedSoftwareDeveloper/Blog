using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetPostByIdHandler : IRequestHandler<GetPostById, PostDto>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetPostByIdHandler(ReadDbContext context, IMapper mapper)
    {
        _posts = context.Posts;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<PostDto> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking().Where(x => x.Id.Equals(request.Id))
            .ProjectTo<PostDto>(_configurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
    #endregion
}
