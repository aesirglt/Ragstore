namespace Totten.Solution.Ragstore.WebApi.SystemConstants;

using Totten.Solution.Ragstore.Infra.Data.Bases;

/// <summary>
/// 
/// </summary>
public class SysConstantDBConfig
{
    /// <summary>
    /// 
    /// </summary>
    public const string DEFAULT_CONNECTION_STRING = $"Server={InfraConstants.MAIN_IP};Database={{dbName}};User Id=sa;Password=Sup3rS3cr3t;TrustServerCertificate=true;";
}
