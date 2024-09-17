using Bogus;
using Person = FakeDataGen.Models.Person;

namespace FakeDataGen.Code
{
    public class FakeDataGenerator
    {
        private readonly Faker<Person> _faker;

        private FakeDataGenerator(Faker<Person> faker)
            => _faker = faker;

        public IEnumerable<Person> Generate(int page, int size, double errorProb)
        {
            var data = _faker.GenerateForever().Skip((page - 1) * size).Take(size).ToList();
            return ApplyErrors(data, errorProb);
        }

        private IEnumerable<Person> ApplyErrors(IEnumerable<Person> data, double errorProb)
        {
            foreach (var person in data)
            {
                IntroduceErrors(person, errorProb);
            }
            return data;
        }

        private void IntroduceErrors(Person person, double errorProb)
        {
            person.FirstName = ErrorGenerator.ApplyErrorsWithProbability(person.FirstName, errorProb, 1, GetLocale());
            person.LastName = ErrorGenerator.ApplyErrorsWithProbability(person.LastName, errorProb, 1, GetLocale());
            person.StreetAddress = ErrorGenerator.ApplyErrorsWithProbability(person.StreetAddress, errorProb, 1, GetLocale());
            person.City = ErrorGenerator.ApplyErrorsWithProbability(person.City, errorProb, 1, GetLocale());
            person.State = ErrorGenerator.ApplyErrorsWithProbability(person.State, errorProb, 1, GetLocale());
            person.Country = ErrorGenerator.ApplyErrorsWithProbability(person.Country, errorProb, 1, GetLocale());
            person.PhoneNumber = ErrorGenerator.ApplyErrorsWithProbability(person.PhoneNumber, errorProb, 1, GetLocale());
        }

        private Locale GetLocale()
            => Utils.GetLocale(_faker.Locale);

        private static string GetLocale(Locale locale)
            => (locale) switch
            {
                Locale.ES => "es_MX",
                Locale.DE => "de_AT",
                Locale.EN => "en_US",
                _ => throw new InvalidOperationException("Invalid enum value.")
            };

        public static FakeDataGenerator Create(int seed, Locale locale)
        {
            int index = 0;
            return new(new Faker<Person>(GetLocale(locale)).UseSeed(seed)
                .RuleFor(p => p.FirstName, (f, p) => f.Name.FirstName())
                .RuleFor(p => p.LastName, (f, p) => f.Name.LastName())
                .RuleFor(p => p.Identifier, (f, p) => f.Random.Guid())
                .RuleFor(p => p.Index, f => index++)
                .RuleFor(p => p.StreetAddress, f => f.Address.StreetAddress())
                .RuleFor(p => p.State, f => f.Address.State())
                .RuleFor(p => p.City, f => f.Address.City())
                .RuleFor(p => p.Country, f => f.Address.LocalizedCountry())
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber()));
        }


    }
}
