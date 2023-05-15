using Volo.Abp.Reflection;

namespace Tasky.RemittanceService.Permissions;

public class RemittanceServicePermissions
{
    public const string GroupName = "RemittanceService";
    public static class Remittances
    {
        public const string Default = GroupName + ".Remittance";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Approved = Default + ".Approved";
        public const string Released = Default + ".Released";
        public const string Ready = Default + ".Ready";

    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(RemittanceServicePermissions));
    }
}
