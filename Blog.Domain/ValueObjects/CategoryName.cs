using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public record CategoryName
{
    public string Value { get; }
    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyCategoryNameException();
        Value = value;
    }

    public static implicit operator string(CategoryName categoryName) => categoryName.Value;
    public static implicit operator CategoryName(string value) => new(value);
}
