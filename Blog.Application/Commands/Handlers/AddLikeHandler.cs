using Blog.Application.Exceptions;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class AddLikeHandler : IRequestHandler<AddLike, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    #endregion

    #region CTORS :
    public AddLikeHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(AddLike request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null) throw new InvalidPostIdException(request.PostId);

        LikeId likeId = new(Guid.NewGuid());
        post.Like(new Domain.Entities.Like(likeId, request.UserId));

        _postRepository.Update(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
