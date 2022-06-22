using Blog.Application.Consts;
using Blog.Application.Exceptions;
using Blog.Application.Services;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class DeletePostHandler : IRequestHandler<DeletePost, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    private readonly IFileManagerService _fileService;
    #endregion

    #region CTORS :
    public DeletePostHandler(IPostRepository postRepository, IFileManagerService fileService)
    {
        _postRepository = postRepository;
        _fileService = fileService;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(DeletePost request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.Id);
        if (post is null) throw new InvalidPostIdException(request.Id);

        _fileService.RemoveFile(post.Image, FileType.BlogImage);

        _postRepository.Delete(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
