Order: 1
Description: An overview of the Blog recipe, including key features and usage notes.
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

## Index and Archive Pages

The Blog recipe is very flexible in terms of how your posts are presented. You can display them on the index page, on an archive page, with or without excerpts, and control things like page counts. This is all managed using the following [settings](/recipes/blog/settings):

* `BlogKeys.IndexPageSize`
* `BlogKeys.IndexPaging`
* `BlogKeys.IndexFullPosts`
* `BlogKeys.ArchivePageSize`
* `BlogKeys.ArchiveExcerpts`
* `BlogKeys.GenerateArchive`

You can display your posts on either the index, a separate archive, or both.

For example, to display the full content of your most recent post on the index and the remaining posts in the archive in pages of 10, add the following to your [configuration file](/docs/usage/configuration):

```
Settings[BlogKeys.IndexPageSize] = 1;
Settings[BlogKeys.IndexFullPosts] = true;
Settings[BlogKeys.ArchivePageSize] = 10;
```

Alternatively, to display all of your posts on the index page in groups of 5 and turn off the archive section, add the following:

```
Settings[BlogKeys.IndexPageSize] = 5;
Settings[BlogKeys.IndexPaging] = true;
Settings[BlogKeys.GenerateArchive] = false;
```

For more information about these settings including their types and default values, see [the settings page](/recipes/blog/settings).

## Redirects

If migrating your site from another generator, the paths may not match up exactly. This recipe supports multiple styles of redirects. To activate, add `RedirectFrom` metadata to the [front matter](/docs/concepts/metadata#front-matter) of each page that should be the endpoint of a redirection. You can control what gets generated to support redirection with the `MetaRefreshRedirects` and `NetlifyRedirects` global metadata values. By default, [meta refresh redirects](https://www.w3.org/TR/WCAG20-TECHS/H76.html) are generated. These are small HTML files that contain a `<meta>` header tag redirecting the client. They are also [supported by Google](https://support.google.com/webmasters/answer/79812) and other search engines.

## Link Validation

The recipe includes the ability to validate both relative (internal) and absolute (external) links. Both features are opt-in given that they add additional time to the generation process. To activate them add `Settings[BlogKeys.ValidateRelativeLinks] = true;` for relative link validation and/or `Settings[BlogKeys.ValidateAbsoluteLinks] = true;` for absolute link validation to your configuration file.

By default validation failures output a warning with details on the link, why it failed, and which files it's in. You can switch to outputting errors (which will usually break the build) by using `Settings[BlogKeys.ValidateLinksAsError] = true;` in your configuration file.

Note that turning on validation for absolute links typically results in a number of falsely reported failures which will need to be checked manually (so it's not recommended to validate absolute links *and* output errors).