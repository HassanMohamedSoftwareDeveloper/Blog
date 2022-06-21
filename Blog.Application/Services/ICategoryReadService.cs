namespace Blog.Application.Services;

public interface ICategoryReadService
{
    Task<bool> ExistsByNameAsync(Guid id, string name);
    Task<bool> CategoryHasPosts(Guid id);
}
