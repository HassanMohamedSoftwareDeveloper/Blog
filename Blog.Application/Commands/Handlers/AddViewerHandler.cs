using Blog.Application.Exceptions;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class AddViewerHandler : IRequestHandler<AddViewer, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    #endregion

    #region CTORS :
    public AddViewerHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(AddViewer request, CancellationToken cancellationToken)
    {

        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null) throw new InvalidPostIdException(request.PostId);

        post.AddViewer();

        _postRepository.Update(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
