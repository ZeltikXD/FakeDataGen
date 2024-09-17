using Bogus.DataSets;

namespace FakeDataGen.Code
{
    public static class StaticData
    {
        public static readonly IReadOnlyList<ListItem<Locale>> _locales = [new("English (United States)", Locale.EN), new("Deutsch (Austria)", Locale.DE), new("Spanish (Mexico)", Locale.ES)];
    }

    public record ListItem<T>(string Name, T Value);
}
