using Blog.Application.Consts;
using Blog.Application.Exceptions;
using Blog.Application.Services;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class UpdatePostHandler : IRequestHandler<UpdatePost, bool>
{
    #region Fields :
    private readonly IPostRepository _postRepository;
    private readonly IFileManagerService _fileService;
    #endregion

    #region CTORS :
    public UpdatePostHandler(IPostRepository postRepository, IFileManagerService fileService)
    {
        _postRepository = postRepository;
        _fileService = fileService;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.Id);
        if (post is null) throw new InvalidPostIdException(request.Id);

        string imageFileName = post.Image;

        if (request.ImageSourceStream != null)
        {
            _fileService.RemoveFile(imageFileName);
            imageFileName = await _fileService.SaveFileAsync(request.ImageSourceStream, FileType.BlogImage);
        }

        post.Update(request.Title, request.Description, request.Tags, request.Body, imageFileName, request.UserId, request.CategoryId);

        _postRepository.Update(post);
        return await _postRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
