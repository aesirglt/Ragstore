namespace Totten.Solution.Ragstore.Domain.Bases;
public class Entity<TEntity, TId>
    where TEntity : Entity<TEntity, TId>
    where TId : notnull
{
    public required TId Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
