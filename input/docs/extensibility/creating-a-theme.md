Title: Creating A Theme
Description: Explains how to create and distribute your own theme.
---
Creating a theme is easy. One of the most helpful things to keep in mind is that there is no behind-the-scenes magic at work. The built-in themes use the exact same mechanisms and conventions you have access to.

A theme [is just a collection of input files](/docs/concepts/themes). They work by being in an input folder at a lower precedence than the main input folder. You can even start working on your theme using files in your normal `input` path and then package them up later.

Once you've got the input files exactly as you like them, package them up as a [NuGet package with content files](http://blog.nuget.org/20160126/nuget-contentFiles-demystified.html). When such a package is included in a Wyam generation (either by using the `--nuget` [command line option](/docs/usage/command-line) or by using the `#nuget` preprocessor directive in a [configuration file](/docs/usage/configuration)), it will automatically make the files in the content folder of the package available as a low precedence input path.

Note that the built-in themes rely on a lookup table to map the theme name such as "CleanBlog" to the matching NuGet package (in this case [Wyam.Blog.CleanBlog](https://www.nuget.org/packages/Wyam.Blog.CleanBlog/)). That's why you can use the `--theme` command line option with a simple theme name for built-in themes. While this lookup table is currently limited only to built-in themes, the net effect is exactly the same as if including the theme package using the NuGet command line option or preprocessor directive. To let others use your theme, just advertise the NuGet package and they can include it via NuGet.