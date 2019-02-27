Title: Version 2.2.3
Published: 2/27/2019
---
Version 2.2.3 is now available.

# Release Notes

## Features

- New `Raw` shortcode useful for escaping inner shortcode syntax

## Refactoring

- Changes the special nested escape processing instruction for shortcodes from a standard processing instruction to `<?* ... ?>`

## Fixes

- Fixes a regression in `AutoLink` where a new document was being returned when it shouldn't have been (#786)
- Moved the `AutoLink` module in docs to execute after all template processing (#786)