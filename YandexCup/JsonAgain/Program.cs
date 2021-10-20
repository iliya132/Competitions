using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace JsonAgain
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstRow = Console.ReadLine();
            int n = int.Parse(firstRow.Split(' ')[0]); //количество фидов магазина 1<=n<=200
            int m = int.Parse(firstRow.Split(' ')[1]); //максимальное количество фидов в итоговом фиде 1<=m<=10*10^4
            Offers[] feeds = new Offers[n]; // входные фиды
            for (int i = 0; i < n; i++)
            {
                feeds[i] = JsonSerializer.Deserialize<Offers>(Console.ReadLine());
            }

            Offers result = new Offers
            {
                OffersArray = feeds.SelectMany(i => i.OffersArray).Take(m).ToArray()
            };

            Console.WriteLine(JsonSerializer.Serialize<Offers>(result));

        }
        public class Offers
        {
            [JsonPropertyName("offers")]
            public Offer[] OffersArray { get; set; }
        }

        public class Offer
        {
            [JsonPropertyName("offer_id")]
            public string OfferId { get; set; }
            [JsonPropertyName("market_sku")]
            public int MarketSku { get; set; }
            [JsonPropertyName("price")]
            public int Price { get; set; }
        }
    }
}
