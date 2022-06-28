using Blog.Application.Consts;

namespace Blog.Application.Services;

public interface IFileManagerService
{
    FileStream FileStream(string fileName);
    Task<string> SaveFileAsync(Stream sourceStream, FileType fileType);
    bool RemoveFile(string fileName);
    string GetFileMime(string fileName);
}