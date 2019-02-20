Title: Version 2.2.0
Published: 2/20/2019
---
Version 2.2.0 is now available.

# Release Notes

## Breaking Changes

- Removes the BookSite recipe and the Velocity theme (#764)

## Features

- Shortcodes! Take a look at [the docs](https://wyam.io/docs/concepts/shortcodes) for more information (#359)
- New `MirrorResources` module to download CDN links and mirror them locally (#781)
- New `IMetadata.String()` extension that takes a formatting function to apply if the key exists in the metadata
- New `IMetadata.TryGetValue<T>()` method to attempt getting a metadata value, but only if it can be converted/cast to `T`
- New `IMetadata` extension methods to make generating XML-to-LINQ trees from metadata values easier

## Fixes

- Fixed a bug when the `Sass` module processes Sass files that have includes which aren't under an `input` folder

## Refactoring

- New `IExecutionContext.HttpClient` and `IExecutionContext.GetHttpClient(HttpMessageHandler)` to manage a single shared `HttpClient` instance
- New support in `Wyam.Testing` and `TestExecutionContext` for testing modules that use a `HttpClient`

# Significant Updates

## Shortcodes!

This is a big new feature with lots of potential. It's a little hard to explain quickly in this blog post, so head over to [the docs](https://wyam.io/docs/concepts/shortcodes) to learn more. I'm excited about where we'll be able to take shortcodes.

## New `MirrorResources` module

Sometimes you have a layout or page that references external JavaScript or stylesheets (for example, from a CDN) but you want to serve those files locally. Bundlers like WebPack help with this, but what if you just want mirror those external resources from your own server and don't want to mess with setting up another tool?

The `MirrorResources` module will locate external stylesheet and script references in your code, copy them to an output folder of your choice (`mirror` by default), and rewrite those `<script>` and `<link>` elements to point to the new local location. You can use it in your own Wyam configurations, though it's not added to the recipes at this time.

## Removal of BookSite recipe and the Velocity theme

The BookSite recipe and it's corresponding Velocity theme have been totally removed from Wyam. I realize this stinks for sites using that recipe, but all the recipes will eventually be merged in v3 (see #668) and while Blog and Docs have enough in common to make merging them fairly easy, BookSite is totally different and I don't see it surviving the merge. Because of that, when combined with the much lower usage numbers, I just don't see the value in spending time continuing to maintain it. If anyone really needs to continue using BookSite, I'd suggest you stay on the most recent version of Wyam that has it (2.1.3), download the code from that release and continue packaging BookSite manually, or port the BookSite code back into a Wyam config file.

