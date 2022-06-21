using Blog.Application.Exceptions;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class AddCommentHandler : IRequestHandler<AddComment, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    #endregion

    #region CTORS :
    public AddCommentHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(AddComment request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null) throw new InvalidPostIdException(request.PostId);

        CommentId commentId = new(Guid.NewGuid());
        post.AddComment(new Domain.Entities.Comment(commentId, request.Message, request.UserId));

        _postRepository.Update(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}