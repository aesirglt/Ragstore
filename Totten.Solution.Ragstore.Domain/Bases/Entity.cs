namespace Totten.Solution.Ragstore.Domain.Bases;
public class Entity<TEntity> where TEntity : Entity<TEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
