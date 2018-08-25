namespace GiphyCli
{
    using System;
    using System.ComponentModel.DataAnnotations;
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
            Console.WriteLine("MARKDOWN:");
            Console.WriteLine($"![{result.Title}]({result.GifUrl})");
            Console.WriteLine("");
            Console.WriteLine("Thanks for using this CLI. Hope you enjoy it.");
            Console.WriteLine("Visit https://github.com/DavidDeSloovere/giphy-cli for comments, issues, ...");
            Console.WriteLine("");
        }
    }
}
