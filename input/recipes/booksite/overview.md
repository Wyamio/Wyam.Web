Order: 1
Description: An overview of the BookSite recipe, including key features and usage notes.
---
The BookSite recipe fills a niche for websites dedicated to book and eBook marketing.

# Key Features

- Define chapter content and/or summaries as either Markdown or Razor files.
- Integrated blog.
- Support for arbitrary custom pages.
- Individual Markdown or Razor files for homepage sections.
- RSS, Atom, and/or RDF feeds for the blog.

# Scaffolding

To scaffold the recipe, run:

```
wyam new -r BookSite
```

This generates a skeleton set of input files that includes a chapter, blog post, custom page, and homepage sections. You can use this as a starting point for your own site.

# Building

To build a site with this recipe, run:

```
wyam -r BookSite
```

# Themes

To select a specific [theme](/recipes/booksite/themes), run:

```
wyam -r BookSite -t Velocity
```

# Usage Notes

## Example

There's an excellent example of a full BookSite at [https://github.com/Wyamio/Wyam/tree/develop/examples/SherlockHolmes](https://github.com/Wyamio/Wyam/tree/develop/examples/SherlockHolmes).

## Blog Post Published Dates

To find the published date for blog posts, the metadata will first be searched for a `Published` value. If one cannot be found (or it can't be converted to a `DateTime`) then the filename of the input file for the post will be checked for a date that appears in the beginning of the file name of the format `YYYY-MM-DD-`.

## Redirects

If migrating your site from another generator, the paths may not match up exactly. This recipe supports multiple styles of redirects. To activate, add `RedirectFrom` metadata to the [front matter](/docs/concepts/metadata#front-matter) of each page that should be the endpoint of a redirection. You can control what gets generated to support redirection with the `MetaRefreshRedirects` and `NetlifyRedirects` global metadata values. By default, [meta refresh redirects](https://www.w3.org/TR/WCAG20-TECHS/H76.html) are generated. These are small HTML files that contain a `<meta>` header tag redirecting the client. They are also [supported by Google](https://support.google.com/webmasters/answer/79812) and other search engines.

## Link Validation

The recipe includes the ability to validate both relative (internal) and absolute (external) links. Both features are opt-in given that they add additional time to the generation process. To activate them add `Settings[BlogKeys.ValidateRelativeLinks] = true;` for relative link validation and/or `Settings[BlogKeys.ValidateAbsoluteLinks] = true;` for absolute link validation to your configuration file.

By default validation failures output a warning with details on the link, why it failed, and which files it's in. You can switch to outputting errors (which will usually break the build) by using `Settings[BlogKeys.ValidateLinksAsError] = true;` in your configuration file.

Note that turning on validation for absolute links typically results in a number of falsely reported failures which will need to be checked manually (so it's not recommended to validate absolute links *and* output errors).