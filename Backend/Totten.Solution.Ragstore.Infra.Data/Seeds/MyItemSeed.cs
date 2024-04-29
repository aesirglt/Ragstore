namespace Totten.Solution.Ragstore.Infra.Data.Seeds;

using Newtonsoft.Json;
using System.Text;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

public class MyItemSeed
{
    public static List<Item> Seed()
    {
        try
        {
            string filePath = ".\\Seeds\\Jsons\\items.json";

            return JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(filePath, Encoding.UTF8)) ?? new();
        }
        catch
        {
            return new List<Item>();
        }
    }
}
