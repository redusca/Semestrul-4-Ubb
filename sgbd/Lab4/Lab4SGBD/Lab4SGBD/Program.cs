using Microsoft.Data.SqlClient;
using System.Data;

namespace Deadlock
{
    class Program
    {
        private const string ConnectionString = "Data Source=DESKTOP-0PG9QNP\\SQLEXPRESS;" +
        "Initial Catalog=StatisticiJocuriVideo;" +
        "Integrated Security=True;" +
        "TrustServerCertificate=True";
        private const int MaxRetries = 3;
        private const int CommandTimeout = 30; 

        static async Task Main(string[] args)
        {
            Task task1 = DeadLockThread("DeadLockOne", "Thread 1");
            Task task2 = DeadLockThread("DeadLockTwo", "Thread 2");

            await Task.WhenAll(task1, task2);

            Console.ReadKey();
        }

        private static async Task DeadLockThread(string procedureName, string threadName)
        {
            int retryCount = 0;
            bool success = false;

            while (!success && retryCount < MaxRetries)
            {
                try
                {
                    retryCount++;
                    Console.WriteLine($"{threadName}: Try {retryCount} , Procedura: {procedureName}");

                    await ExecProcedura(procedureName, threadName);

                    success = true;
                    Console.WriteLine($"{threadName}: Procedura {procedureName} s-a executat");
                }
                catch (SqlException ex) when (IsDeadlockException(ex))
                {
                    Console.WriteLine($"{threadName}: DEADLOCK , retryCount : {retryCount}! SqlErrorCode: {ex.Number}");

                    if (retryCount >= MaxRetries)
                    {
                        Console.WriteLine($"{threadName}: Last Try {procedureName} Abandon");
                    }
                    else
                    {
                        int delayMs = 2000;
                        Console.WriteLine($"{threadName}: Retray in {delayMs / 1000} seconds");
                        await Task.Delay(delayMs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{threadName}: Error: {ex.Message}");
                    break;
                }
            }
        }

        private static async Task ExecProcedura(string procedureName, string threadName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine($"{threadName}: Connection successful");

                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = CommandTimeout;

                    Console.WriteLine($"{threadName}: Exec Proc {procedureName}...");
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"{threadName}: Proc {procedureName} Done");
                }
            }
        }

        private static bool IsDeadlockException(SqlException ex)
        {
            return ex.Number == 1205;
        }
    }
}