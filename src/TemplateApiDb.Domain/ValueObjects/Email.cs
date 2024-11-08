using TemplateApiDb.Library;

namespace TemplateApiDb.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; private set; }

        public Email(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static explicit operator Email(string email)
        {
            return new Email(email);
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}
