Order: 1
---
The Blog recipe is probably the best way to get an experience similar to other blog-based static site generators like Jekyll. It will pick up all Markdown and Razor files in the `/posts` directory as blog posts, and all Markdown and Razor files in the root site directory as additional information pages. It will also generate Atom and RSS feeds for your posts, as well as create archives by date and by tags.

To scaffold the recipe, run:

```
wyam new -r Blog
```

To build a site with the recipe, run:

```
wyam -r Blog
```