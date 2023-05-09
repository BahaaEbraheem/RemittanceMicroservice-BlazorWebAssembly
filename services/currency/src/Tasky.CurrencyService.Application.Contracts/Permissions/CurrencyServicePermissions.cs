using Volo.Abp.Reflection;

namespace Tasky.CurrencyService.Permissions;

public class CurrencyServicePermissions
{
    public const string GroupName = "CurrencyService";
    public static class Currencies
    {
        public const string Default = GroupName + ".Currencies";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CurrencyServicePermissions));
    }
}
