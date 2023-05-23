namespace Tasky.AmlService;

public static class AmlServiceDbProperties
{
    public static string DbTablePrefix { get; set; } =null;

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "AmlService";
}
