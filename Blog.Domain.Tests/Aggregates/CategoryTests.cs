using Blog.Domain.Aggregates;
using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Tests.Aggregates;

public class CategoryTests
{
    [Fact]
    public void CategoryId_ShouldBeFail_With_EmptyCategoryIdExceptionWithMessage_WhenIdIsEmpty()
    {
        //Arrange
        Guid idValue = Guid.Empty;
        //Act
        var action = () => { CategoryId id = new(idValue); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Category Id can't be empty.")
            .And.Should().BeOfType(typeof(EmptyCategoryIdException));
    }
    [Fact]
    public void CategoryName_ShouldBeFail_With_EmptyCategoryNameExceptionWithMessage_WhenNameIsEmpty()
    {
        //Arrange
        string nameValue = string.Empty;
        //Act
        var action = () => { CategoryName name = new(String.Empty); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Category Name can't be empty.")
            .And.Should().BeOfType(typeof(EmptyCategoryNameException));
    }

    [Fact]
    public void CategoryId_ShouldBeSuccess_WhenIdHasValue()
    {
        //Arrange
        Guid idValue = Guid.NewGuid();
        //Act
        var action = () => { CategoryId id = new(idValue); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void CategoryName_ShouldBeSuccess_WhenNameHasValue()
    {
        //Arrange
        string nameValue = "Programming";
        //Act
        var action = () => { CategoryName name = new(nameValue); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }

    [Fact]
    public void Category_ShouldBeFail_With_EmptyCategoryIdExceptionWithMessage_WhenIdIsEmpty()
    {
        //Arrange
        Guid id = Guid.Empty;
        string name = "Programming";
        //Act
        var action = () => { Category category = new(new CategoryId(id), new CategoryName(name)); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Category Id can't be empty.")
            .And.Should().BeOfType(typeof(EmptyCategoryIdException));
    }
    [Fact]
    public void Category_ShouldBeFail_With_EmptyCategoryNameExceptionWithMessage_WhenNameIsEmpty()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string name = string.Empty;
        //Act
        var action = () => { Category category = new(new CategoryId(id), new CategoryName(name)); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Category Name can't be empty.")
            .And.Should().BeOfType(typeof(EmptyCategoryNameException));
    }
    [Fact]
    public void Category_ShouldBeSuccess_WhenIdHasValueAndNameHasValue()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string name = "Programing";
        //Act
        var action = () => { Category category = new(new CategoryId(id), new CategoryName(name)); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
}
