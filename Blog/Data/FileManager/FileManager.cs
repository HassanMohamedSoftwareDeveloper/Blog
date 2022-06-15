using PhotoSauce.MagicScaler;

namespace Blog.Data.FileManager;

public class FileManager : IFileManager
{
    private readonly string _imagePath;

    public FileManager(IConfiguration configuration)
    {
        _imagePath = configuration["Path:Images"];
    }

    public FileStream ImageStream(string image) =>
        new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
    public string SaveImageAsync(IFormFile image)
    {
        var savePath = Path.Combine(_imagePath);
        if (Directory.Exists(savePath) is false)
            Directory.CreateDirectory(savePath);
        //IE C:/user/foo/image.jpg
        var mime = image.FileName[image.FileName.LastIndexOf('.')..];//Range Operator
        var fileName = $"img_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}{mime}";

        using var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create);
        //await image.CopyToAsync(fileStream);
        MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());
        return fileName;
    }
    public bool RemoveImage(string image)
    {
        try
        {
            var file = Path.Combine(_imagePath, image);
            if (File.Exists(file))
                File.Delete(file);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private ProcessImageSettings ImageOptions()
    {
        var imageSettings = new ProcessImageSettings
        {
            Width = 800,
            Height = 500,
            ResizeMode = CropScaleMode.Stretch,
            EncoderOptions = new JpegEncoderOptions
            {
                Quality = 100,
                Subsample = ChromaSubsampleMode.Subsample420,
            }
        };
        imageSettings.TrySetEncoderFormat(ImageMimeTypes.Jpeg);
        return imageSettings;
    }
}
