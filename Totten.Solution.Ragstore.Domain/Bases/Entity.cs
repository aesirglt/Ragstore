namespace Totten.Solution.Ragstore.Domain.Bases;
public class Entity<TEntity> where TEntity : Entity<TEntity>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
