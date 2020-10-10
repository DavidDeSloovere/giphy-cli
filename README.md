# giphy-cli
Giphy CLI as global dotnet tool - get that url or markdown fast

Comments, ideas, bug reports and PR are welcome here.

![.NET Core CI](https://github.com/DavidDeSloovere/giphy-cli/workflows/.NET%20Core%20CI/badge.svg)

## Get started

Head over to [GiphyCli on NuGet](https://www.nuget.org/packages/GiphyCli) or continue reading:


You'll need the .NET Core SDK [3.1](https://www.microsoft.com/net/download) or newer to install global tools.

Install the Giphy CLI with this command:

```
> dotnet tool install --global GiphyCli
```

To search for a gif, simply use

```
> giphy lolcats
```

## Usage

```
> giphy cheeseburger
```

That's it; no options needed. Just pass the search as the first argument.

```
> giphy "awesome cheeseburger"
```

So it would look something like this:

![Screenshot Giphy CLI](README-screenshot.png)

And allow you to just paste the markdown that results in this:

![high five amy poehler GIF](https://media1.giphy.com/media/120jXUxrHF5QJ2/giphy.gif)

## Features

- Interactive prompt: Open giphy.com URL, copy .gif deeplink or __copy markdown to clipboard__
- Preview image in iTerm2 (PR by https://github.com/slang25) 
