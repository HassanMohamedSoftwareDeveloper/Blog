using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Tests.Aggregates;

public class PostTests
{
    #region Title_Tests :
    [Fact]
    public void PostId_ShouldThrowException_EmptyTitleException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        Guid value = Guid.Empty;
        //Act
        var action = () => { PostId id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Post Id can't be empty.")
            .And.Should().BeOfType(typeof(EmptyPostIdException));
    }
    [Fact]
    public void PostId_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        Guid value = Guid.NewGuid();
        //Act
        var action = () => { PostId id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void PostId_ShouldBe_Return_String()
    {
        Guid guid = Guid.NewGuid();
        //Arrange
        PostId postId = new(guid);
        //Act
        Guid guidPostId = postId;
        //Assert
        Assert.Equal(guid, guidPostId);
    }
    [Fact]
    public void PostId_ShouldBe_Return_Title()
    {
        //Arrange
        Guid guidPostId = Guid.NewGuid();
        //Act
        PostId postId = guidPostId;
        //Assert
        Assert.Equal(guidPostId, postId.Value);
        Assert.Equal(new PostId(guidPostId), postId);
    }
    #endregion

    #region Title_Tests :
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
    #endregion

    #region Description_Tests :
    [Fact]
    public void Description_ShouldThrowException_EmptyDescriptionException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = string.Empty;
        //Act
        var action = () => { Description id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Description can't be empty.")
            .And.Should().BeOfType(typeof(EmptyDescriptionException));
    }
    [Fact]
    public void Description_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        string value = "Description";
        //Act
        var action = () => { Description id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void Description_ShouldBe_Return_String()
    {
        //Arrange
        Description description = new("Description");
        //Act
        string stringDescription = description;
        //Assert
        Assert.Equal("Description", stringDescription);
    }
    [Fact]
    public void Description_ShouldBe_Return_Description()
    {
        //Arrange
        string stringDescription = "Description";
        //Act
        Description description = stringDescription;
        //Assert
        Assert.Equal("Description", description.Value);
        Assert.Equal(new Description("Description"), description);
    }
    #endregion

    #region Tag_Tests :
    [Fact]
    public void Tag_ShouldThrowException_EmptyTagException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = string.Empty;
        //Act
        var action = () => { Tag id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Tag can't be empty.")
            .And.Should().BeOfType(typeof(EmptyTagException));
    }
    [Fact]
    public void Tag_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        string value = "Tag";
        //Act
        var action = () => { Tag id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void Tag_ShouldBe_Return_ListOfString()
    {
        //Arrange
        Tag tag = new("Tag1,Tag2,Tag3");
        //Act
        List<string> tagList = tag;
        //Assert
        var tags = new List<string>()
        {
            "Tag1",
            "Tag2",
            "Tag3",
        };
        Assert.Equal(tags, tagList);
    }
    [Fact]
    public void Tagn_ShouldBe_Return_Tag()
    {
        //Arrange
        List<string> tagList = new()
        {
            "Tag1",
            "Tag2",
            "Tag3",
        };
        //Act
        Tag tag = tagList;
        //Assert
        Assert.Equal("Tag1,Tag2,Tag3", tag.Value);
        Assert.Equal(new Tag("Tag1,Tag2,Tag3"), tag);
    }
    #endregion

    #region Body_Tests :
    [Fact]
    public void Body_ShouldThrowException_EmptyBodyException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = string.Empty;
        //Act
        var action = () => { Body id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Body can't be empty.")
            .And.Should().BeOfType(typeof(EmptyBodyException));
    }
    [Fact]
    public void Body_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        string value = "Body";
        //Act
        var action = () => { Body id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void Body_ShouldBe_Return_String()
    {
        //Arrange
        Body Body = new("Body");
        //Act
        string stringBody = Body;
        //Assert
        Assert.Equal("Body", stringBody);
    }
    [Fact]
    public void Body_ShouldBe_Return_Body()
    {
        //Arrange
        string stringBody = "Body";
        //Act
        Body body = stringBody;
        //Assert
        Assert.Equal("Body", body.Value);
        Assert.Equal(new Body("Body"), body);
    }
    #endregion

    #region Image_Tests :
    [Fact]
    public void Image_ShouldThrowException_EmptyImageException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = string.Empty;
        //Act
        var action = () => { Image id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Image can't be empty.")
            .And.Should().BeOfType(typeof(EmptyImageException));
    }
    [Fact]
    public void Image_ShouldThrowException_InvalidImageException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = "Image";
        //Act
        var action = () => { Image id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Invalid Image, Image must be like  *.jpg")
            .And.Should().BeOfType(typeof(InvalidImageException));
    }
    [Fact]
    public void Imagey_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        string value = "Image.jpg";
        //Act
        var action = () => { Image id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void Image_ShouldBe_Return_String()
    {
        //Arrange
        Image image = new("Image.jpg");
        //Act
        string stringImage = image;
        //Assert
        Assert.Equal("Image.jpg", stringImage);
    }
    [Fact]
    public void Image_ShouldBe_Return_Body()
    {
        //Arrange
        string stringImage = "Image.jpg";
        //Act
        Image image = stringImage;
        //Assert
        Assert.Equal("Image.jpg", image.Value);
        Assert.Equal(new Image("Image.jpg"), image);
    }
    #endregion
}
