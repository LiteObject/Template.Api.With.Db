using TemplateApiDb.Library;

namespace TemplateApiDb.Domain.ValueObjects
{
    public class Phone : ValueObject
    {
        public string Value { get; private set; }

        public Phone(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static explicit operator Phone(string phone)
        {
            return new Phone(phone);
        }

        public static implicit operator string(Phone phone)
        {
            return phone.Value;
        }
    }
}
