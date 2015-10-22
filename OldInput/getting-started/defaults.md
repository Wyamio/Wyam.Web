Title: Defaults
Description: The default configuration if you don't supply one of your own.
Order: 5
---
It's entirely possible to use Wyam without ever worring about configuration files. If you don't create or specify one, two pipelines are used by default:

- **Content** is run first. It can be thought of in three phases:
  1. First the [ReadFiles](/modules/ReadFiles) module searches for all `.md` Markdown files in the input folder (usually `.\Input`). These are processed for any YAML front matter which is added to the document metadata by using the [FrontMatter](/modules/frontmatter) module and passing the front matter to the [Yaml](/modules/yaml) module. Then HTML is rendered from the Markdown content using the [Markdown](/modules/markdown) module.
  2. The second phase adds documents to the sequence with the [Concat](/modules/concat) module by using the [ReadFiles](/modules/ReadFiles) module to search for all `.cshtml` Razor templates *that don't start with an underscore* (as these are usually layouts or partials). These are processed for any YAML front matter which is added to the document metadata by using the [FrontMatter](/modules/frontmatter) module and passing the front matter to the [Yaml](/modules/yaml) module.
  3. Finally, *both* the Markdown documents and the Razor documents are evaluated by the [Razor](/modules/razor) module to convert Razor templates to HTML. The results are written to disk as `.html` files by the [WriteFiles](/modules/writefiles) module.

- **Resources** simply copies all files that don't end in `.md` or `.cshtml` from the input folder to the output folder.

Essentially this means that if you put a combination of Markdown, Razor, and other resource files in your input folder, they'll get handled correctly and a finished site will get placed in your output folder. The combined power of Razor (with layouts, etc.) and ease of Markdown make this default configuration a great publishing setup right off the bat. In fact, this site actually uses a minor modification of this very configuration.

For reference, the full default configuration script is:

```
Pipelines.Add("Content",
    ReadFiles("*.md"),
    FrontMatter(Yaml()),
    Markdown(),
    Concat(
        ReadFiles("*.cshtml").Where(x => Path.GetFileName(x)[0] != '_'),
        FrontMatter(Yaml())		
    ),
    Razor(),
    WriteFiles(".html")
);

Pipelines.Add("Resources",
    CopyFiles("*").Where(x => Path.GetExtension(x) != ".cshtml" && Path.GetExtension(x) != ".md")
);
```