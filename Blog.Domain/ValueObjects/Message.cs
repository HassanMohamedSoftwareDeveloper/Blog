using Blog.Domain.Exceptions;

namespace Blog.Domain.ValueObjects
{
    public record Message
    {
        public string Value { get; }
        public Message(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new EmptyMessageException();
            Value = value;
        }

        public static implicit operator string(Message message) => message.Value;
        public static implicit operator Message(string value) => new(value);
    }
}
