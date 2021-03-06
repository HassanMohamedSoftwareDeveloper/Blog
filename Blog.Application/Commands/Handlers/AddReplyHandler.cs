using Blog.Application.Exceptions;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class AddReplyHandler : IRequestHandler<AddReply, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    #endregion

    #region CTORS :
    public AddReplyHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(AddReply request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null) throw new InvalidPostIdException(request.PostId);

        CommentId replyId = new(Guid.NewGuid());
        post.AddReply(request.CommentId, new Domain.Entities.Reply(replyId, request.Message, request.UserId));

        _postRepository.Update(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
