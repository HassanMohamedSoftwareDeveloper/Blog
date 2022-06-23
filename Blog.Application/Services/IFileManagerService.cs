using Blog.Application.Consts;

namespace Blog.Application.Services;

public interface IFileManagerService
{
    FileStream FileStream(string fileName);
    Task<string> SaveFileAsync(string sourcePath, FileType fileType);
    bool RemoveFile(string fileName);
}