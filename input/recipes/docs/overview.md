Order: 1
---
The Docs recipe is designed to cover many different use cases for documentation sites including any combination of blog, content, and API documentation. The generated site will automatically adapt to the input files and will hide navigation sections and other functionality if those types of files aren't available. For example, if you don't specify code or assemblies for the API section, the generated site will still be great for content pages and/or blog posts.

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

To find the published date for blog posts, the metadata will first be searched for a `Published` value. If one cannot be found (or it can't be converted to a `DateTime`) then the filename of the input file for the post will be checked for a date that appears in the beginning of the file name in the format `YYYY-MM-DD-`.

## Namespace Documentation Comments

The XML documentation standard is ambiguous on how you should defined comments for namespaces. Wyam will pick up standard XML documentation comments on any arbitrary namespace usage within your code. Alternatively, you can use a class named `NamespaceDoc` and place your namespace XML documentation comments on that class. Note that the former will not work if generating documentation from an assembly since the MSBuild XML documentation file does not output XML documentation comments for namespace usages.