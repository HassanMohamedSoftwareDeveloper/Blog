using Blog.Application.Consts;
using Blog.Application.Services;

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
        throw new NotImplementedException();
    }
    #endregion
}
