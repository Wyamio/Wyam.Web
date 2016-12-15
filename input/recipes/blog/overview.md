Order: 1
---
The Blog recipe is probably the best way to get an experience similar to other blog-based static site generators like Jekyll. It will pick up all Markdown and Razor files in the `/posts` directory as blog posts, and all Markdown and Razor files in the root site directory as additional information pages. It will also generate Atom and RSS feeds for your posts, as well as create archives by date and by tags.

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