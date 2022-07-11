using Blog.Application.Consts;
using Blog.Application.Services;
using Blog.Infrastructure.Consts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Blog.Infrastructure.Services;

internal sealed class FileManagerService : IFileManagerService
{
    #region Fields :
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileManagerService> _logger;
    #endregion

    #region CTORS :
    public FileManagerService(IConfiguration configuration, ILogger<FileManagerService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    #endregion

    #region Methods :
    public FileStream FileStream(string fileName)
    {
        string fileBasePath = GetFilePath(fileName);
        string fileFullPath = Path.Combine(fileBasePath, fileName);
        if (File.Exists(fileFullPath) is false)
            fileFullPath = Path.Combine(GetDefaultPath(), DefaultFileName(fileName));
        return new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
    }
    public bool RemoveFile(string fileName)
    {
        try
        {
            string filePath = GetFilePath(fileName);
            var file = Path.Combine(filePath, fileName);
            if (File.Exists(file))
                File.Delete(file);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Remove image.");
            return false;
        }
    }
    public async Task<string> SaveFileAsync(Stream sourceStream, FileType fileType)
    {
        try
        {
            if (sourceStream is null) return DefaultFileName(fileType);
            string savePath = GetFilePath(fileType);

            CreateDirectory(savePath);

            var fileName = GetFileName(fileType);

            using var image = Image.Load(sourceStream);

            ResizeOptions resizeOptions = new()
            {
                Size = new Size(256, 256),
                Mode = ResizeMode.Stretch,
            };

            image.Mutate(x => x.Resize(resizeOptions));

            using var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create);

            await image.SaveAsJpegAsync(fileStream);

            return fileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image.");
            return DefaultFileName(fileType);
        }
    }

    public string GetFileMime(string fileName)
    {
        return fileName[(fileName.LastIndexOf('.') + 1)..];
    }
    #endregion

    #region Helpers :
    private static void CreateDirectory(string path)
    {
        if (Directory.Exists(path) is false)
            Directory.CreateDirectory(path);
    }
    private static string GetFileName(FileType fileType)
    {
        string fileName = $"{{0}}{DateTime.Now:dd-MM-yyyy-HH-mm-ss}.jpg";
        return fileType switch
        {
            FileType.BlogImage => string.Format(fileName, FileNamePrefix.BlogPrefix),
            FileType.UserImage => string.Format(fileName, FileNamePrefix.UserPrefix),
            _ => string.Empty
        };
    }
    private string GetFilePath(FileType fileType)
    {
        return fileType switch
        {
            FileType.BlogImage => Path.Combine(_configuration["Path:BlogImages"]),
            FileType.UserImage => Path.Combine(_configuration["Path:UserImages"]),
            _ => GetDefaultPath(),
        };
    }
    private string GetFilePath(string fileName)
    {
        var filePrefix = fileName[0..(fileName.IndexOf('_') + 1)];//Range Operator
        return filePrefix switch
        {
            FileNamePrefix.BlogPrefix => Path.Combine(_configuration["Path:BlogImages"]),
            FileNamePrefix.UserPrefix => Path.Combine(_configuration["Path:UserImages"]),
            _ => GetDefaultPath(),
        };
    }

    private static string DefaultFileName(FileType fileType)
    {
        return fileType switch
        {
            FileType.BlogImage => DefaultFileNames.Blog,
            FileType.UserImage => DefaultFileNames.User,
            _ => string.Empty
        };
    }
    private static string DefaultFileName(string fileName)
    {
        var filePrefix = fileName[0..(fileName.IndexOf('_') + 1)];//Range Operator
        return filePrefix switch
        {
            FileNamePrefix.BlogPrefix => DefaultFileNames.Blog,
            FileNamePrefix.UserPrefix => DefaultFileNames.User,
            _ => string.Empty
        };
    }
    private string GetDefaultPath()
    {
        return Path.Combine(_configuration["Path:DefaultImages"]);
    }
    #endregion
}
