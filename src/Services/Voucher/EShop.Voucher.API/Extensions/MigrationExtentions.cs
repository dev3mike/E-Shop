using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace EShop.Voucher.API.Extensions
{
    public static class MigrationExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry=0){

            var retryToCheckAvailalability = retry.Value;

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");

            try
            {
                logger.LogInformation("Postgresql: Migration started...");

                
                using var dbConnection = new NpgsqlConnection(connectionString);

                dbConnection.Open();

                using var command = new NpgsqlCommand {
                    Connection = dbConnection
                };

                // Drop tables
                ExecuteCommand( command,
                                query: "DROP TABLE IF EXISTS Voucher");

                // Create vouchers table
                ExecuteCommand( command,
                                query: "CREATE TABLE Voucher (" +
                                            "Id SERIAL PRIMARY KEY," +
                                            "Code VARCHAR(24) NOT NULL," +
                                            "Amount INT," +
                                            "IsUsed BOOLEAN," +
                                            "UsedDate TIMESTAMP DEFAULT NULL" +
                                          ")");

                // Insert sample vouchers
                ExecuteCommand( command,
                                query: "INSERT INTO Voucher (Code, Amount, IsUsed) VALUES ('NEW_CUSTOMER', 20, false)");


                logger.LogInformation("Postgresql: Migration done ✔");
            }
            catch (Exception)
            {
                if(retry < 10)
                {
                    System.Threading.Thread.Sleep(2000);
                    logger.LogInformation("Postgresql: Retrying...");

                    retry++;
                    MigrateDatabase<TContext>(host, retryToCheckAvailalability);
                }
                else
                {
                    logger.LogInformation("Postgresql: Migration Failed...");
                }
                
            }

            return host;
        }

        private static void ExecuteCommand(NpgsqlCommand command, string query)
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
    }
}
