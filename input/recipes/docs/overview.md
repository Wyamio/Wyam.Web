Order: 1
Description: An overview of the docs recipe, including key features and usage notes.
---
The Docs recipe is designed to cover many different use cases for documentation sites including any combination of blog, content, and API documentation. The generated site will automatically adapt to the input files and will hide navigation sections and other functionality if those types of files aren't available. For example, if you don't specify code or assemblies for the API section, the generated site will still be great for content pages and/or blog posts.

# Key Features

- Nested documentation content pages.
- Blog posts with optional category and author.
- Paged blog archives for posts, categories, authors, and dates.
- Posts and pages can be in [Markdown](/modules/markdown) or [Razor](/modules/razor).
- API documentation from source files.
- API documentation from assemblies (with or without XML documentation file from MSBuild).
- Static site searching for API types.
- Meta-refresh redirects and/or a Netlify redirect file.
- RSS, Atom, and/or RDF feeds.

# Scaffolding

To scaffold the recipe, run:

```
wyam new -r Docs
```

This generates a skeleton set of input files that includes a blog post and a content page. You can use this as a starting point for your own site.

# Building

To build a site with this recipe, run:

```
wyam -r Docs
```

# Usage Notes

## Blog Post Published Dates

To find the published date for blog posts, the metadata will first be searched for a `Published` value. If one cannot be found (or it can't be converted to a `DateTime`) then the filename of the input file for the post will be checked for a date that appears in the beginning of the file name of the format `YYYY-MM-DD-`.

## Namespace Documentation Comments

The XML documentation standard is ambiguous on how you should defined comments for namespaces. Wyam will pick up standard XML documentation comments on any arbitrary namespace usage within your code. Alternatively, you can use a class named `NamespaceDoc` and place your namespace XML documentation comments on that class. Note that the former will not work if generating documentation from an assembly since the MSBuild XML documentation file does not output XML documentation comments for namespace usages.

## Redirects

If migrating your site from another generator, the paths may not match up exactly. This recipe supports multiple styles of redirects. To activate, add `RedirectFrom` metadata to the [front matter](/docs/concepts/metadata#front-matter) of each page that should be the endpoint of a redirection. You can control what gets generated to support redirection with the `MetaRefreshRedirects` and `NetlifyRedirects` global metadata values. By default, [meta refresh redirects](https://www.w3.org/TR/WCAG20-TECHS/H76.html) are generated. These are small HTML files that contain a `<meta>` header tag redirecting the client. They are also [supported by Google](https://support.google.com/webmasters/answer/79812) and other search engines.