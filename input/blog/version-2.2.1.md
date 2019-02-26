Title: Version 2.2.1
Published: 2/26/2019
---
Version 2.2.1 is now available.

# Release Notes

## Features

- New `Highlight` shortcode
- New `YouTube` shortcode
- New `Giphy` shortcode
- New `CodePen` shortcode
- Improvements to `Sass` module processing of `@import` including a new `.WithImportPath()` delegate that can fine-tune import path locations

## Refactoring

- Refactored `Embed` shortcode to make derived oEmbed shortcodes easier to implement
- Refactored `HttpClient` support in `ExecutionContext` to work more like `HttpClientFactory` by sharing a `HttpMessageHandler` instead of a `HttpClient` instance