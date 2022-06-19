using Blog.Domain.Repositories;
using Moq;

namespace Blog.Domain.Tests.Repositories;

public class CategoryRepositoryTests
{
    #region Fields :
    private readonly Mock<ICategoryRepository> _mockRepo;
    private readonly ICategoryRepository _repo;
    #endregion

    #region CTORS :
    public CategoryRepositoryTests()
    {
        _mockRepo = new Mock<ICategoryRepository>();
        _repo = new Mock<ICategoryRepository>().Object;
    }
    #endregion

    #region Tests :
    [Fact]
    public async Task Get()
    {
        //var id = new CategoryId(Guid.NewGuid());
        //var name = new CategoryName("Programming");
        //var category = new Category(id, name);
        //var ccc = Guid.NewGuid();
        //Func<Category, bool> c = x => x.Id == new CategoryId(ccc);
        //_mockRepo.Setup(x => x.GetIDAsync(It.IsAny<Func<Category, bool>>())).Returns(category);
        //var cat2 = GetId(id);
        //var cat = _repo.GetIDAsync(It.IsAny<Func<Category, bool>>());//GetId(ccc);
        //_mockRepo.Verify(x => x.GetIDAsync(It.IsAny<Func<Category, bool>>()), Times.Once);
    }
    [Fact]
    public void Create()
    {
        //var action = () => { _repo.Create(Arg.Any<Domain.Aggregates.Category>()); };
        //action.Should().NotThrow<BlogException>();
        //action.Should().NotThrow<Exception>();
    }
    [Fact]
    public void Update()
    {
        //var action = () => { _repo.Update(Arg.Any<Domain.Aggregates.Category>()); };
        //action.Should().NotThrow<BlogException>();
        //action.Should().NotThrow<Exception>();
    }
    [Fact]
    public void Delete()
    {
        //var action = () => { _repo.Delete(Arg.Any<Domain.Aggregates.Category>()); };
        //action.Should().NotThrow<BlogException>();
        //action.Should().NotThrow<Exception>();
    }
    #endregion
}
