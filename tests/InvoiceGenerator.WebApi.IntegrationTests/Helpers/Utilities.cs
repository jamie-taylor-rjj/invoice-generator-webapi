using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.Domain;
using InvoiceGenerator.Domain.Models;

namespace InvoiceGenerator.WebApi.IntegrationTests.Helpers;

[ExcludeFromCodeCoverage]
public static class Utilities
{
    public static Guid KnownGoodClientId = new("3334D659-95EB-4863-9034-0F1A00F8FAA2");
    public static void InitializeDbForTests(InvoiceDbContext db)
    {
        db.Clients.AddRange(SeedClients());
        db.SaveChanges();
    }

    private static List<Client> SeedClients() =>
        new()
        {
            new Client
            {
                ClientId = KnownGoodClientId,
                ClientName = "Wayne Enterprises",
                ClientAddress = "Gotham City, and that",
                ContactEmail = "bruce.wayne@wayne.enterprises",
                ContactName = "Bruce Wayne"
            }
        };
}