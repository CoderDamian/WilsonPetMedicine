
namespace Wpm.Clinic.Domain.ValueObjects
{
    public record Text
    {
        public string Value { get; init; } = string.Empty;

        public Text(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException(value);

            if (value.Length > 500)
                throw new ArgumentException("texto es demasiado largo");
        }

        public static implicit operator Text(string value)
        {
            return new Text(value);
        }
    }
}
