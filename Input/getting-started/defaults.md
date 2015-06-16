Title: Defaults
Description: The default configuration if you don't supply one of your own.
Order: 4
---
It's entirely possible to use Wyam without ever worring about configuration files. If you don't create or specify one, three pipelines are used by default:

- **Markdown** is run first. It searches for all `.md` Markdown files in the input folder (usually `.\Input`). Then it processes any YAML front matter and adds it to the document metadata. Then it renders HTML from the Markdown content. Finally, *it also runs the rendered HTML through the Razor engine* so that any layouts get applied. Once done, files are written in the output folder (usually `.\Output`) with the same base filename and a `.html` extension.

- **Razor** is run next. It searches for all `.cshtml` Razor templates *that don't start with an underscore* (as these are usually layouts or partials). Then it processes any YAML front matter and adds it to the document metadata. The Razor templates are then run through the Razor engine and the resulting HTML is written in the output folder (usually `.\Output`) with the same base filename and a `.html` extension.

- **Resources** is run last and simply copies all files that don't end in `.md` or `.cshtml` from the input folder to the output folder.

Essentially this means that if you put a combination of Markdown, Razor, and other resource files in your input folder, they'll get handled correctly and a finished site will get placed in your output folder. The combined power of Razor (with layouts, etc.) and ease of Markdown make this default configuration a great publishing setup right off the bat. In fact, this site actually uses a minor modification of this very configuration.

For reference, the full default configuration script is:

```
Pipelines.Add("Markdown",
    ReadFiles(@@"*.md"),
    FrontMatter(Yaml()),
    Markdown(),
    Razor(),
    WriteFiles(".html")
);

Pipelines.Add("Razor",
    ReadFiles(@@"*.cshtml").Where(x => Path.GetFileName(x)[0] != '_'),
    FrontMatter(Yaml()),
    Razor(),
    WriteFiles(".html")
);

Pipelines.Add("Resources",
    CopyFiles(@@"*").Where(x => Path.GetExtension(x) != ".cshtml" && Path.GetExtension(x) != ".md")
);