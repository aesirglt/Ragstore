namespace Totten.Solution.Ragstore.Infra.Data.Bases;
public class InfraConstants
{
    public static readonly string MAIN_IP = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "postgres";
    public static readonly string STORE_DB_NAME = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "ragstore";
    public static readonly string PRINCIPAL_SERVER_DB_NAME = Environment.GetEnvironmentVariable("POSTGRES_DB_PRINCIPAL_SERVERDB") ?? "brothor";
}
