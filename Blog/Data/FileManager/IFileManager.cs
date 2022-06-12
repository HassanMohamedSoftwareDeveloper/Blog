namespace Blog.Data.FileManager;

public interface IFileManager
{
    FileStream ImageStream(string image);
    string SaveImageAsync(IFormFile image);
    bool RemoveImage(string image);
}
