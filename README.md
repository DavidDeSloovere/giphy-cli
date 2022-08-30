# giphy-cli

A CLI, published as Docker image and .NET Global tool, to search for a gif on Giphy and optionally open the link in the browser or copy the link or markdown to the clipboard.

Was featured in a presentation about **.NET interactive notebooks** at .NET Conf 2020: https://youtu.be/938jBJ-tK3c?t=1025

There is an example notebook included [dotnet-interactive-notebook-sample.ipynb](https://github.com/DavidDeSloovere/giphy-cli/blob/main/docs/dotnet-interactive-notebook-sample.ipynb)

Comments, ideas, bug reports and PR are welcome here.

![.NET Core CI](https://github.com/DavidDeSloovere/giphy-cli/workflows/.NET%20Core%20CI/badge.svg)

## Docker

You can run this CLI via Docker. This will output markdown and a link to giphy.com.

`docker run --rm -it giphy-cli:latest "lolcats"`

`docker run --rm -it ghcr.io/daviddesloovere/giphy-cli:latest "lolcats"`

## .NET global tool

Head over to [GiphyCli on NuGet](https://www.nuget.org/packages/GiphyCli) or continue reading:

You'll need the [.NET 6 runtime](https://www.microsoft.com/net/download) or newer.

Install the Giphy CLI with this command:

```
> dotnet tool install --global GiphyCli
```

Update the Giphy CLI with this command:

```
> dotnet tool update --global Giphycli
```

To search for a gif, simply use

```
> giphy lolcats
```

## Usage

```
> giphy cheeseburger
> giphy "awesome cheeseburger"
```

Output markdown only, great for using in notebooks.

```
> giphy cheeseburger -m
> giphy cheeseburger --markdown
```

Giphy CLI now an includes interactive prompt.

![Screenshot Giphy CLI](https://raw.githubusercontent.com/DavidDeSloovere/giphy-cli/main/README-screenshot.png)

Have markdown copied to clipboard _et voila_.

![will ferrell yes GIF](https://raw.githubusercontent.com/DavidDeSloovere/giphy-cli/main/README-example.gif)

## Features

- Interactive prompt: Open giphy.com URL, copy .gif deeplink or **copy markdown to clipboard**
- Preview image in iTerm2 (PR by https://github.com/slang25)
