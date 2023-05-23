using Volo.Abp.Reflection;

namespace Tasky.AmlService.Permissions;

public class AmlServicePermissions
{
    public const string GroupName = "AmlService";
    public static class AmlRemittances
    {
        public const string Default = GroupName + ".AmlRemittance";
        public const string Check = Default + ".Check";

    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AmlServicePermissions));
    }
}
