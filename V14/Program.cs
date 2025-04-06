using System.Text.Json;

namespace V14
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            BasicImage image = await GetRandomDogImageAsync();
            Console.WriteLine("Random Dog Image URL: " + image.Url);
        }

        private static async Task<BasicImage> GetRandomDogImageAsync()
        {
            string url = "https://dog.ceo/api/breeds/image/random";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            DogApiResponse apiResponse = JsonSerializer.Deserialize<DogApiResponse>(result);

            if (apiResponse == null || string.IsNullOrEmpty(apiResponse.message))
            {
                throw new Exception("Failed");
            }

            return new BasicImage { Url = apiResponse.message };
        }
    }
}
