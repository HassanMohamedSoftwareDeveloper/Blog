using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects;

public class CategoryId
{
    public Guid Value { get; }
    public CategoryId(Guid value)
    {
        if (value == default || value == Guid.Empty) throw new EmptyCategoryIdException();
        Value = value;
    }

    public static implicit operator Guid(CategoryId categoryId) => categoryId.Value;
    public static implicit operator CategoryId(Guid value) => new(value);
}
