namespace Totten.Solution.Ragstore.Infra.Data.Seeds;

using Newtonsoft.Json;
using System.Text;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;

public class MyItemSeed
{
    public static List<Item> Seed()
    {
        try
        {
            string filePath = ".\\Seeds\\Jsons\\items.json";

            return JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(filePath, Encoding.UTF8)) ?? new();
        }
        catch (Exception ex)
        {
            return new List<Item>();
        }

        //using var streamReader = new StreamReader(filePath);
        //using var jsonReader = new JsonTextReader(streamReader);
        //jsonReader.SupportMultipleContent = true;
        //var serializer = new JsonSerializer();

        //while (jsonReader.Read())
        //{
        //    if (jsonReader.TokenType == JsonToken.StartObject)
        //    {
        //        var obj = serializer.Deserialize<Item>(jsonReader);
        //        yield return obj;
        //    }
        //}
    }
}
