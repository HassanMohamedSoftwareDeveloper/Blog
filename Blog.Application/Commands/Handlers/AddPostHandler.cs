using Blog.Application.Consts;
using Blog.Application.Services;
using Blog.Domain.Aggregates;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class AddPostHandler : IRequestHandler<AddPost, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    private readonly IFileManagerService _fileService;
    #endregion

    #region CTORS :
    public AddPostHandler(IPostRepository postRepository, IFileManagerService fileService)
    {
        _postRepository = postRepository;
        _fileService = fileService;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(AddPost request, CancellationToken cancellationToken)
    {
        string imageFileName = await _fileService.SaveFileAsync(request.ImageSourceStream, FileType.BlogImage);

        var id = new PostId(Guid.NewGuid());
        var post = new Post(id, request.Title, request.Description, request.Tags, request.Body, imageFileName, request.UserId, request.CategoryId);

        _postRepository.Create(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
