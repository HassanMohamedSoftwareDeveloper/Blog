using Blog.Application.Consts;

namespace Blog.Application.Services;

public interface IFileService
{
    FileStream FileStream(string fileName, FileType fileType);
    string SaveFile(string sourcePath, FileType fileType);
    bool RemoveFile(string fileName, FileType fileType);
}