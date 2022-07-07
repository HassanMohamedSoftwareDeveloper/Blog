using Blog.Application.Exceptions;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class RemoveLikeHandler : IRequestHandler<RemoveLike, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    #endregion

    #region CTORS :
    public RemoveLikeHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(RemoveLike request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null) throw new InvalidPostIdException(request.PostId);

        var like = post.UnLike(request.UserId);

        if (like is null) return true;

        _postRepository.DeleteLike(like);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
