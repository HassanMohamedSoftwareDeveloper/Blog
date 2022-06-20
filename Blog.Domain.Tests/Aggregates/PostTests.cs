using Blog.Domain.Aggregates;
using Blog.Domain.Exceptions;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Tests.Aggregates;

public class PostTests
{
    #region PostId_Tests :
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
    public void PostId_ShouldBe_Return_PostId()
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

    #region UserId_Tests :
    [Fact]
    public void UserId_ShouldThrowException_EmptyUserIdException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        string value = string.Empty;
        //Act
        var action = () => { UserId id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("User Id can't be empty.")
            .And.Should().BeOfType(typeof(EmptyUserIdException));
    }
    [Fact]
    public void UserId_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        string value = "UserId";
        //Act
        var action = () => { UserId id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void UserId_ShouldBe_Return_String()
    {
        //Arrange
        UserId userId = new("UserId");
        //Act
        string stringUserId = userId;
        //Assert
        Assert.Equal("UserId", stringUserId);
    }
    [Fact]
    public void UserId_ShouldBe_Return_Title()
    {
        //Arrange
        string stringUserId = "UserId";
        //Act
        UserId userId = stringUserId;
        //Assert
        Assert.Equal("UserId", userId.Value);
        Assert.Equal(new UserId("UserId"), userId);
    }
    #endregion

    #region CategoryId_Tests :
    [Fact]
    public void CategoryId_ShouldThrowException_EmptyCategoryIdException_WithMessage_WhenValueIsEmpty()
    {
        //Arrange
        Guid value = Guid.Empty;
        //Act
        var action = () => { CategoryId id = new(value); };
        //Assert
        action.Should().Throw<BlogException>().WithMessage("Category Id can't be empty.")
            .And.Should().BeOfType(typeof(EmptyCategoryIdException));
    }
    [Fact]
    public void CategoryId_ShouldNotThrowException_WhenHasValue()
    {
        //Arrange
        Guid value = Guid.NewGuid();
        //Act
        var action = () => { CategoryId id = new(value); };
        //Assert
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void CategoryId_ShouldBe_Return_String()
    {
        Guid guid = Guid.NewGuid();
        //Arrange
        CategoryId categoryId = new(guid);
        //Act
        Guid guidCategoryId = categoryId;
        //Assert
        Assert.Equal(guid, guidCategoryId);
    }
    [Fact]
    public void CategoryId_ShouldBe_Return_CategoryId()
    {
        //Arrange
        Guid guidCategoryId = Guid.NewGuid();
        //Act
        CategoryId categoryId = guidCategoryId;
        //Assert
        Assert.Equal(guidCategoryId, categoryId.Value);
        Assert.Equal(new CategoryId(guidCategoryId), categoryId);
    }
    #endregion

    #region Post_Tests :
    [Fact]
    public void Initiate_Post_ShouldBe_Has_Values()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);

        Assert.Equal(postId, post.Id);
        //Assert.Equal(title, post._title);
        //Assert.Equal(description, post._description);
        //Assert.Equal(tag, post._tag);
        //Assert.Equal(body, post._body);
        //Assert.Equal(image, post._image);
        //Assert.Equal(userId, post._userId);
        //Assert.Equal(categoryId, post._categoryId);
        //Assert.True(post._created.Date == DateTime.Now.Date);
        //Assert.Null(post._updated);


    }
    [Fact]
    public void Post_AddComment_ShouldbeThrowException_EmptyCommentIdException_WithMessage_When_CommentIdIsEmpty()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);
        var action = () =>
        {
            var comment = new Entities.Comment(new CommentId(Guid.Empty), new Message("Message"), new UserId(Guid.NewGuid().ToString()));
            post.AddComment(comment);
        };
        action.Should().Throw<BlogException>().WithMessage("Comment Id can't be empty.")
          .And.Should().BeOfType(typeof(EmptyCommentIdException));
    }
    [Fact]
    public void Post_AddComment_ShouldbeThrowException_EmptyMessageException_WithMessage_When_MessageIsEmpty()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);
        var action = () =>
        {
            var comment = new Entities.Comment(new CommentId(Guid.NewGuid()), new Message(String.Empty), new UserId(Guid.NewGuid().ToString()));
            post.AddComment(comment);
        };
        action.Should().Throw<BlogException>().WithMessage("Message can't be empty.")
          .And.Should().BeOfType(typeof(EmptyMessageException));
    }
    [Fact]
    public void Post_AddComment_ShouldbeThrowException_EmptyUserIdException_WithMessage_When_UserIdIsEmpty()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);
        var action = () =>
        {
            var comment = new Entities.Comment(new CommentId(Guid.NewGuid()), new Message("Message"), new UserId(String.Empty));
            post.AddComment(comment);
        };
        action.Should().Throw<BlogException>().WithMessage("User Id can't be empty.")
          .And.Should().BeOfType(typeof(EmptyUserIdException));
    }
    [Fact]
    public void Post_AddComment_ShouldbeNotThrowException_When_HasAllValues()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);
        var action = () =>
        {
            var comment = new Entities.Comment(new CommentId(Guid.NewGuid()), new Message("Message"), new UserId(Guid.NewGuid().ToString()));
            post.AddComment(comment);
        };
        action.Should().NotThrow<BlogException>();
    }
    [Fact]
    public void Post_AddComment_Shouldbe_AddedToCommentsList()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);
        var action = () =>
        {
            var comment = new Entities.Comment(new CommentId(Guid.NewGuid()), new Message("Message"), new UserId(Guid.NewGuid().ToString()));
            post.AddComment(comment);
        };
        action.Should().NotThrow<BlogException>();
        //Assert.Single(post._comments);
        //var comment = post._comments.Single();
        //Assert.NotNull(comment.Id);
    }
    [Fact]
    public void Post_AddReply_Shouldbe_AddedToCommentsList()
    {
        var postId = new PostId(Guid.NewGuid());
        var title = new Title("Title");
        var description = new Description("Description");
        var tag = new Tag("Tag1,Tag2");
        var body = new Body("Body");
        var image = new Image("Image.jpg");
        var userId = new UserId(Guid.NewGuid().ToString());
        var categoryId = new CategoryId(Guid.NewGuid());

        var post = new Post(postId, title, description, tag, body, image, userId, categoryId);
        var action = () =>
        {
            var commentId = new CommentId(Guid.NewGuid());
            var comment = new Entities.Comment(commentId, new Message("Message"), new UserId(Guid.NewGuid().ToString()));
            post.AddComment(comment);
            post.AddReply(commentId, new Entities.Reply(new CommentId(Guid.NewGuid()), new Message("Reply"), new UserId(Guid.NewGuid().ToString())));
        };
        action.Should().NotThrow<BlogException>();
        //Assert.Single(post._comments);
        //var comment = post._comments.Single();
        //Assert.NotNull(comment.Id);
        //Assert.Single(comment._replies);
        //var reply = comment._replies.Single();
        //Assert.NotNull(reply.Id);
    }
    #endregion
}
