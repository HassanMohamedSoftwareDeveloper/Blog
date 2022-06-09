namespace Blog.Data.FileManager;

public class FileManager : IFileManager
{
    private readonly string _imagePath;

    public FileManager(IConfiguration configuration)
    {
        _imagePath = configuration["Path:Images"];
    }

    public FileStream ImageStream(string image)
    {
        return new FileStream(Path.Combine(_imagePath, image),FileMode.Open,FileAccess.Read);
    }

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        try
        {
            var savePath = Path.Combine(_imagePath);
            if (Directory.Exists(savePath) is false)
                Directory.CreateDirectory(savePath);
            //IE C:/user/foo/image.jpg
            var mime = image.FileName[image.FileName.LastIndexOf('.')..];//Range Operator
            var fileName = $"img_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}{mime}";

            using var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create);
            await image.CopyToAsync(fileStream);
            return fileName;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "Error";
        }
    }
}
