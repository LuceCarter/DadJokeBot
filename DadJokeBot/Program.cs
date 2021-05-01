using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace DadJokeBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new System.Threading.Timer(async (e) =>
            {
                await WriteDadJokeToFile();
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
           //Task.Run(async () => await WriteDadJokeToFile());
            Console.ReadLine();
        }

        private static async Task WriteDadJokeToFile()
        {
            var joke = "";
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "text/plain");
                client.DefaultRequestHeaders.Add("User-Agent", "LuceCarter Stream Bot");

                Console.WriteLine("Fetching Dad Joke...");
                var dadJoke = await client.GetAsync("https://icanhazdadjoke.com/");
                joke = dadJoke.Content.ReadAsStringAsync().Result;  
            }
            
            try
            {
                await File.WriteAllTextAsync("joke.txt", joke);

                var soundPlayer = new SoundPlayer();
                soundPlayer.SoundLocation = "joke.wav";
                soundPlayer.Play();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
