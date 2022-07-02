using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace InvoiceGenerator.WebApi.Helpers;

[ExcludeFromCodeCoverage]
public static class CommonHelpers
{
    public static string GetAssemblyName()
    {
        return Assembly.GetExecutingAssembly()?.GetName()?.Name ?? "Unknown";
    }
}