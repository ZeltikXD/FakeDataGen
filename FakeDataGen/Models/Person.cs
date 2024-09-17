namespace FakeDataGen.Models
{
    public class Person
    {
        public int Index { get; set; }

        public Guid Identifier { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public string FullAddress => $"{StreetAddress}, {City}, {State}, {Country}";
    }
}
