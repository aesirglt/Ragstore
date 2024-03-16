namespace Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Domain.Bases;

public class Item : Entity<Item>
{
    public string Mercant { get; set; } = null!;
    public double Price { get; set; }
    public DateTime Date { get; set; }
}
