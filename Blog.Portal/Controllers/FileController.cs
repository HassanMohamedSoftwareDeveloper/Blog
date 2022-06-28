using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Portal.Controllers;

[ApiController]
public class FileController : ControllerBase
{
    #region Fields :
    private readonly IFileManagerService _fileManager;
    #endregion

    #region CTORS :
    public FileController(IFileManagerService fileManager)
    {
        _fileManager = fileManager;
    }
    #endregion

    #region Actions :
    [HttpGet("/Image/{image}")]
    public IActionResult Image(string image) => new FileStreamResult(_fileManager.FileStream(image), $"image/{_fileManager.GetFileMime(image)}");
    #endregion
}
