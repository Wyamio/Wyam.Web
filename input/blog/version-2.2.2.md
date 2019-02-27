Title: Version 2.2.2
Published: 2/27/2019
---
Version 2.2.2 is now available.

# Release Notes

## Features

<?! Raw ?><?# Raw ?>

- The `Include` shortcode now tracks what file it's been called from (including nested includes) and attempts to first resolve includes relative to the current file
- Shortcodes are now evaluated recursively and can be nested (but must use the same pre vs. post rendering delimiter as their parent)
- Shortcodes can now be evaluated _before_ rendering with the `<?! ... /?>` syntax as well as after rendering with the existing `<?# ... /?>` syntax

<?#/ Raw ?><?!/ Raw ?>

## Fixes

- Temporary workaround for shortcodes not working under certain conditions after HTML processing (#784)