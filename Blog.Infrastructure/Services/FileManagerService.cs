using Blog.Application.Consts;
using Blog.Application.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Blog.Infrastructure.Services;

public class FileManagerService : IFileManagerService
{
    #region Methods :
    public FileStream FileStream(string fileName, FileType fileType)
    {
        throw new NotImplementedException();
    }
    public bool RemoveFile(string fileName, FileType fileType)
    {
        throw new NotImplementedException();
    }
    public string SaveFile(string sourcePath, FileType fileType)
    {
        using var image = Image.Load(sourcePath);
        ResizeOptions resizeOptions = new()
        {
            Size = new Size(256, 256),
            Mode = ResizeMode.Stretch,
        };
        image.Mutate(x => x.Resize(resizeOptions));


        image.Save()"filename");
    }
    #endregion
}
