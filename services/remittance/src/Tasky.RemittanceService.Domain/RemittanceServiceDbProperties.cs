namespace Tasky.RemittanceService;

public static class RemittanceServiceDbProperties
{
    public static string? DbTablePrefix { get; set; } = null;

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "RemittanceService";
    public const int MaxLength = 1000000000;
}
