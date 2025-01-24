namespace Totten.Solution.Ragstore.Infra.Data.Bases;
public class InfraConstants
{
    public static readonly string MAIN_IP = Environment.GetEnvironmentVariable("MAIN_IP") ?? "192.168.4.109";
    public static readonly string STORE_DB_NAME = Environment.GetEnvironmentVariable("STORE_DB_NAME") ?? "RagnaStore";
}
