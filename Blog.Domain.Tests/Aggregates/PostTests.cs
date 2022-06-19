using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Tests.Aggregates;

public class PostTests
{
    [Fact]
    public void Title_ShouldThrowException_EmptyTitleException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = string.Empty;
        //Act
        var action = () => { Title id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Title can't be empty.")
            .And.Should().BeOfType(typeof(EmptyTitleException));
    }
    [Fact]
    public void Title_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        string value = "Title";
        //Act
        var action = () => { Title id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void Title_ShouldBe_Return_String()
    {
        //Arrange
        Title title = new("Title1");
        //Act
        string stringTitle = title;
        //Assert
        Assert.Equal("Title1", stringTitle);
    }
    [Fact]
    public void Title_ShouldBe_Return_Title()
    {
        //Arrange
        string stringTitle = "Title1";
        //Act
        Title title = stringTitle;
        //Assert
        Assert.Equal("Title1", title.Value);
        Assert.Equal(new Title("Title1"), title);
    }
}
