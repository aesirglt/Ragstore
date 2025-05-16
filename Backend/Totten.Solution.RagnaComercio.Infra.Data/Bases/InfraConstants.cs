namespace Totten.Solution.RagnaComercio.Infra.Data.Bases;
public class InfraConstants
{
    public static readonly string MAIN_IP = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "192.168.1.208";
    public static readonly string STORE_DB_NAME = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "ragnacomercio";
    public static readonly string PRINCIPAL_SERVER_DB_NAME = Environment.GetEnvironmentVariable("POSTGRES_DB_PRINCIPAL_SERVERDB") ?? "latamro";
}
