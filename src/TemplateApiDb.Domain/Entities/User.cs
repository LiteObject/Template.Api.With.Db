using TemplateApiDb.Domain.ValueObjects;

namespace TemplateApiDb.Domain.Entities
{
    public class User : BaseEntity
    {
        public Name Username { get; set; }

        public Name? FirstName { get; set; }

        public Name? LastName { get; set; }

        public Email Email { get; set; }

        public Phone? PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}