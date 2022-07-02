using System.Reflection;

namespace InvoiceGenerator.WebApi.Helpers;

public static class CommonHelpers
{
    public static string GetAssemblyName()
    {
        return Assembly.GetExecutingAssembly()?.GetName()?.Name ?? "Unknown";
    }
}