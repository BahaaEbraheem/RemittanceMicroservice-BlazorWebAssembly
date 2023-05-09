using Volo.Abp.Reflection;

namespace Tasky.CustomerService.Permissions;

public class CustomerServicePermissions
{
    public const string GroupName = "CustomerService";
    public static class Customers
    {
        public const string Default = GroupName + ".Customers";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CustomerServicePermissions));
    }
}
