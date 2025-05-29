using System.Reflection;
using DbUp;

namespace NextRef.Infrastructure.DataAccess.Migrations;
public static class DbUpRunner
{
    public static void Run(string connectionString)
    {
        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Database upgrade failed:");
            Console.WriteLine(result.Error);
            Console.ResetColor();
            throw new Exception("Database upgrade failed.", result.Error);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Database upgrade successful!");
        Console.ResetColor();
    }
}