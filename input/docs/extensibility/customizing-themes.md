Title: Customizing Themes
Description: Tips for customizing recipes by modifying their themes.
---
While you can easily modify the logic in a [recipe](/docs/concepts/recipes) by [customizing recipe pipelines](/docs/extensibility/customizing-recipe-pipelines), it's also easy to adjust the presentation of [themes](/docs/concepts/themes) without having to [create a new one from scratch](/docs/extensibility/creating-a-theme) (though that's certainly a viable option as well).

When customizing a theme, it's important to keep in mind that themes are just files that get combined with the other files in your input folder as if they were alongside your other input files. Any time a file in your input folder has the same name as a file in the theme, the file in your input folder takes precedence.

To see which files constitute a theme, take a look at the [themes folder in the Wyam GitHub repository](https://github.com/Wyamio/Wyam/tree/develop/themes). Select the theme file(s) you want to modify or replace and copy them to your input folder. In addition to placing theme overrides in your input folder (typically named "input" unless you specify a different name), you can also place them in a folder named "theme" to keep theme overrides and additions separate from your input content.

Once the specific theme files you want to modify or replace are in your input or local theme folder, you can edit them in any way you see fit. The versions from your local folders will take precedence over the packaged theme and your changes will be included in the build.