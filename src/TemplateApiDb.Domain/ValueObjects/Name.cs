using TemplateApiDb.Library;

namespace TemplateApiDb.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; private set; }

        public Name(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static explicit operator Name(string name)
        {
            return new Name(name);
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }
    }
}
