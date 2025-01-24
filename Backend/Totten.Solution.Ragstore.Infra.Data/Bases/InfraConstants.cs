namespace Totten.Solution.Ragstore.Infra.Data.Bases;
public class InfraConstants
{
    public static readonly string MAIN_IP = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "192.168.1.209";
    public static readonly string STORE_DB_NAME = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "ragnastore";
}
