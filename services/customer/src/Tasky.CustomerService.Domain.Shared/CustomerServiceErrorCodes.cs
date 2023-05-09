namespace Tasky.CustomerService;

public static class CustomerServiceErrorCodes
{
    public const string CustomerAlreadyExists = "RMS:00002";
    public const string BirthDateExeption = "RMS:00003";
    public const string RemittanceAlreadyApproved = "RMS:00004";
    public const string CustomerAlreadyUsedInRemittance = "RMS:00006";
    public const string CustomerDontPassBecauseHisAgeSmallerThan18 = "RMS:00007";
}
