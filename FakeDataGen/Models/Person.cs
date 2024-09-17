namespace FakeDataGen.Models
{
    public class Person
    {
        public int Index { get; set; }

        public Guid Identifier { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

    }
}
