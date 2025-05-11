namespace Totten.Solution.RagnaComercio.WebApi.Dtos;
/// <summary>
/// 
/// </summary>
public class PaginationDto<T> 
{
    /// <summary>
    /// 
    /// </summary>
    public long TotalCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public T[] Data { get; set; }
}
