# InvoiceGenerator.WebApi

## User Secrets

In order to hide the connection string from config (as this is an open source repo), the connection string has been hidden in a [User Secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets). All user secrets are loaded in _AFTER_ appsettings.json files, so this works brilliantly for local development.

To add the connection string to a user secret:

- Open a terminal in the `InvoiceGenerator.WebApi` directory
- Run `dotnet user-secrets init`
  - You should be told "The MSBuild project 'InvoiceGenerator.WebApi.csproj' has already been initialized with a UserSecretsId."
  - This is good
- Run `dotnet user-secrets set "ConnectionStrings:invoiceConnectionString" "YOUR CONNECTION STRING HERE"`
  - Replacing "YOUR CONNECTION STRING HERE" with the actual connection string
  - This will place the connection string in a user secret
  - Only someone running this application, when logged into your machine as you will get the connection string
  - The connection string will be injected at application boot time

To check that the connection string was added correctly:

- Open a terminal in the `InvoiceGenerator.WebApi` directory
- Run `dotnet user-secrets list`
- The output should be `ConnectionStrings:invoiceConnectionString = YOUR CONNECTION STRING HERE`
  - Where `YOUR CONNECTION STRING HERE` is the connection string you provided when setting the user secret

To remove the connection string secret:

- Open a terminal in the `InvoiceGenerator.WebApi` directory
- Run `dotnet user-secrets remove "ConnectionStrings:invoiceConnectionString"`
- This will remove the connection string from your user secrets
  - You will either have to replace this or set it in the appsettings.json file
