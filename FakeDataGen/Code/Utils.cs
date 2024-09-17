using Bogus.DataSets;

namespace FakeDataGen.Code
{
    public static class Utils
    {
        public static string LocalizedCountry(this Address address)
            => GetCountry(GetLocale(address.Locale));

        public static string GetCountry(Locale locale)
            => (locale) switch
            {
                Locale.ES => "México",
                Locale.DE => "Österreich",
                Locale.EN => "United States",
                _ => throw new InvalidOperationException("Invalid enum value.")
            };

        public static Locale GetLocale(string locale)
            => (locale) switch
            {
                "en_US" => Locale.EN,
                "de_AT" => Locale.DE,
                "es_MX" => Locale.ES,
                _ => throw new InvalidOperationException("Invalid enum value.")
            };
    }
}
