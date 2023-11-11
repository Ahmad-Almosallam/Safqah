
# Safqah Application Setup Guide

This guide provides step-by-step instructions for setting up and running the Safqah application. Ensure you have Docker and .NET Core SDK installed on your system before proceeding.

## Initial Setup

1. **Start Services with Docker:**
   ```bash
   docker-compose up --build
   ```

2. **Install .NET Entity Framework (EF) Core Tool:**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. **Database Migrations:**
   Run the following commands to apply migrations for each service in the project.

   - Auth API:
     ```bash
     cd services/Safqah.Auth
     dotnet ef database update
     cd ../..
     ```

   - Investors API:
     ```bash
     cd services/Safqah.Investor
     dotnet ef database update
     cd ../..
     ```

   - Opportunities API:
     ```bash
     cd services/Safqah.Opportunities
     dotnet ef database update
     cd ../..
     ```

   - Payment API:
     ```bash
     cd services/Safqah.Payment
     dotnet ef database update
     cd ../..
     ```

   - Wallet API:
     ```bash
     cd services/Safqah.Wallet
     dotnet ef database update
     cd ../..
     ```

## Testing the APIs

To test the application's functionality, follow these steps:

1. **Register an Investor:**
   Use the Investor Service to register a new investor.

2. **Authenticate:**
   Obtain an authentication token through the Auth Service.

3. **Create an Opportunity:**
   Utilize the Opportunity Service to create a new investment opportunity.

4. **Add Funds:**
   Use the Payment Service to add funds to the investor's balance.

5. **Invest in an Opportunity:**
   Finally, use the Opportunity Service to invest in an opportunity.
