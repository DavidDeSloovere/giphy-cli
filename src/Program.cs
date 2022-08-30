namespace GiphyCli
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Net.Http;
    using McMaster.Extensions.CommandLineUtils;
    using TextCopy;

    [Command(Description = "Global CLI to quickly get a Giphy link or markdown for your search (which should always be lolcats).")]
    public class Program
    {
        // one API key for now
        private const string ApiKey = "1TePlQM14HIjQLf8QyivWGH9NwAVQXsd";

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Argument(0, Description = "The search to execute.")]
        [Required]
        public string Search { get; }

        [Option(ShowInHelpText = true, ShortName = "m", LongName = "markdown", Description = "Output only markdown")]
        public bool Markdown { get; set; }

        private void OnExecute()
        {
            if (!this.Markdown)
            {
                Console.WriteLine("");
                Colorful.Console.WriteAscii("GIPHY CLI");
                Console.WriteLine("");
                Console.WriteLine($"Searching giphy.com API for `{this.Search}`...");
                Console.WriteLine("");
            }

            var client = new GiphyApi(ApiKey);
            var result = client.Search(this.Search);
            if (result == null)
            {
                Console.WriteLine("Oh no. Didn't get any results back. What crazy thing are you searching for?");
                Console.WriteLine("");
                return;
            }

            var markdownText = $"![{result.Title}]({result.GifUrl})";
            if (this.Markdown)
            {
                Console.WriteLine(markdownText);
                return;
            }

            var gifUrlText = $"{result.GifUrl}";

            Console.WriteLine($"> Found `{result.Title}`");
            Console.WriteLine($"{result.Url}");
            Console.WriteLine("");

            Console.WriteLine("> GIF URL");
            Console.WriteLine(gifUrlText);
            Console.WriteLine("");

            Console.WriteLine("> MARKDOWN");
            Console.WriteLine(markdownText);
            Console.WriteLine("");

            if (Environment.GetEnvironmentVariable("TERM_PROGRAM") == "iTerm.app")
            {
                Console.Write("\u001B]1337");
                Console.Write(";File=;inline=1:");
                using (var httpClient = new HttpClient())
                {
                    var bytes = httpClient.GetByteArrayAsync(result.GifUrl).GetAwaiter().GetResult();
                    Console.Write(Convert.ToBase64String(bytes));
                }
                Console.Write("\u0007");
                Console.WriteLine("");
            }

            Console.WriteLine("");
            // Awesome lib: https://github.com/shibayan/Sharprompt
            var value = Sharprompt.Prompt.Select<ActionSelectOptions>("What should I do next?");
            switch (value)
            {
                case ActionSelectOptions.OpenGiphyCom:
                    OpenBrowser(result.Url);
                    break;

                case ActionSelectOptions.CopyUrl:
                    ClipboardService.SetText(gifUrlText);
                    break;

                case ActionSelectOptions.CopyMarkdown:
                    ClipboardService.SetText(markdownText);
                    break;
            }

            Console.WriteLine("");
            Console.WriteLine("Thanks for using the Giphy CLI for .NET. Visit https://github.com/DavidDeSloovere/giphy-cli for comments, issues, ...");

        }

        public static void OpenBrowser(string url)
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
