namespace Blog.Data.FileManager;

public interface IFileManager
{
    FileStream ImageStream(string image);
    Task<string> SaveImageAsync(IFormFile image);
}
