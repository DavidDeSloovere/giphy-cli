namespace GiphyCli
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using McMaster.Extensions.CommandLineUtils;

    [Command(Description = "Global CLI to quickly get a Giphy link or markdown for your search (which should always be lolcats).")]
    public class Program
    {
        // one API key for now
        private static readonly string ApiKey = "1TePlQM14HIjQLf8QyivWGH9NwAVQXsd";

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Argument(0, Description = "A positional parameter that must be specified.\nThe search to execute.")]
        [Required]
        public string Search { get; }

        [Option(Description = "When using iTerm2, this tool will display the image in output, you can disable this behaviour with this flag.")]
        public bool DisableITermImages { get; }

        // [Option(Description = "An optional parameter, with a default value.\nThe number of times to say hello.")]
        // [Range(1, 1000)]
        // public int Count { get; } = 1;

        private void OnExecute()
        {
            Console.WriteLine("");
            Console.WriteLine($"Searching giphy.com API for `{this.Search}`...");

            var client = new GiphyApi(ApiKey);
            var result = client.Search(this.Search);
            if (result == null)
            {
                Console.WriteLine("Oh no. Didn't get any results back. What crazy thing are you searching for?");
                Console.WriteLine("");
                return;
            }

            Console.WriteLine($"Found `{result.Title}` at {result.Url}");
            Console.WriteLine("");

            Console.WriteLine("GIF URL");
            Console.WriteLine($"{result.GifUrl}");
            Console.WriteLine("");
        
            if (!DisableITermImages && Environment.GetEnvironmentVariable("TERM_PROGRAM") == "iTerm.app")
            {
                var esc = "\u001B]1337";
                Console.Write(esc);
                Console.Write(";File=;inline=1:");
                using (var httpClient = new HttpClient())
                {
                    var bytes = httpClient.GetByteArrayAsync(result.GifUrl).GetAwaiter().GetResult();
                    Console.Write(Convert.ToBase64String(bytes));
                }
                Console.Write("\u0007");
                Console.WriteLine("");
            }

            Console.WriteLine("MARKDOWN:");
            Console.WriteLine($"![{result.Title}]({result.GifUrl})");
            Console.WriteLine("");
            Console.WriteLine("Thanks for using this CLI. Hope you enjoy it.");
            Console.WriteLine("Visit https://github.com/DavidDeSloovere/giphy-cli for comments, issues, ...");
            Console.WriteLine("");
        }
    }
}
