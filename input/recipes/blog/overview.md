Order: 1
Description: An overview of the blog recipe, including key features and usage notes.
---
The Blog recipe is probably the best way to get an experience similar to other blog-based static site generators like Jekyll. It will pick up all Markdown and Razor files in the `/posts` directory as blog posts, and all Markdown and Razor files in the root site directory as additional information pages. It will also generate Atom and RSS feeds for your posts, as well as create archives by date and by tags.

# Key Features

- Content pages in addition to blog posts.
- Posts and pages can be in [Markdown](/modules/markdown) or [Razor](/modules/razor).
- Tag engine, including a tag archive and tag lists on every post.
- Automatic archive page.
- Meta-refresh redirects and/or a Netlify redirect file.
- RSS, Atom, and/or RDF feeds.

# Scaffolding

To scaffold the recipe, run:

```
wyam new -r Blog
```

This generates a skeleton set of input files that includes a blog post and a content page. You can use this as a starting point for your own site.

# Building

To build a site with this recipe, run:

```
wyam -r Blog
```

# Themes

To select a specific [theme](/recipes/blog/themes), run:

```
wyam -r Blog -t CleanBlog
```

# Usage Notes

## Blog Post Published Dates

To find the published date for blog posts, the metadata will first be searched for a `Published` value. If one cannot be found (or it can't be converted to a `DateTime`) then the filename of the input file for the post will be checked for a date that appears in the beginning of the file name of the format `YYYY-MM-DD-`.

## Redirects

If migrating your site from another generator, the paths may not match up exactly. This recipe supports multiple styles of redirects. To activate, add `RedirectFrom` metadata to the [front matter](/docs/concepts/metadata#front-matter) of each page that should be the endpoint of a redirection. You can control what gets generated to support redirection with the `MetaRefreshRedirects` and `NetlifyRedirects` global metadata values. By default, [meta refresh redirects](https://www.w3.org/TR/WCAG20-TECHS/H76.html) are generated. These are small HTML files that contain a `<meta>` header tag redirecting the client. They are also [supported by Google](https://support.google.com/webmasters/answer/79812) and other search engines.