namespace Totten.Solution.RagnaComercio.WebApi.SystemConstants;

using Totten.Solution.RagnaComercio.Infra.Data.Bases;

/// <summary>
/// 
/// </summary>
public class SysConstantDBConfig
{
    static string user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "admin";
    static string pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "Sup3rS3cr3t";

    /// <summary>
    /// 
    /// </summary>
    public static readonly string DEFAULT_CONNECTION_STRING = $"Host={InfraConstants.MAIN_IP};Port=5432;Username={user};Password={pass};Database={{dbName}}";
}
