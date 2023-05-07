namespace Tasky.CurrencyService;

public static class CurrencyServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "CurrencyService";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "CurrencyService";
}
