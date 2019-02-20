Title: Shortcodes
Description: Shortcodes are small but powerful macros that can generate content in your documents.
Order: 4
---
Wyam supports several templating engines like Markdown and Razor, but sometimes you need to generate some content regardless of the templating language you're using. Shortcodes are small text macros that can do big things and work across any templating engine. Wyam comes with several helpful shortcodes built-in and it's easy to add your own.

# Using Shortcodes

Shortcodes are a special type of XML processing instruction delimited with `<?#` and `?>`. This syntax allows the shortcodes to "fall-through" most templating engines like Markdown since those languages ignore XML processing instructions. Every shortcode has a name, often has parameters, and can optionally contain content depending on how and what the shortcode renders.

A shortcode must always be closed with a `/` (similar to XML elements). If the shortcode contains no content, it can be self-closing by using a trailing slash: `<?# ShortcodeName /?>`. If the shortcode does contain content, it must be closed with a matching shortcode: `<?# ShortcodeName ?>content<?#/ ShortcodeName ?>`.

## Shortcode Names

By convention shortcode names use CamelCase due to the fact that most of them come from .NET classes, which are also named with CamelCase. That said, the shortcode name is case-insensitive so if you prefer to use some other casing convention, you can.

## Parameters

Some shortcodes accept parameters which can be either positional, named, or a mixture of both depending on the shortcode. Shortcode parameters appear in the opening shortcode element and are delimited from the shortcode name and each other by whitespace. If the value of a parameter requires whitespace of it's own it can be enclosed in quotes. Parameter names do not need to appear in any specific order and are specified as `key=value` (or `key="value"` if the value contains whitespace).

**A single unnamed parameter value:**

```
<?# ShortcodeName parameter-value /?>
```

**A single unnamed quoted parameter value:**

```
<?# ShortcodeName "parameter value" /?>
```

**Multiple unnammed positional parameter values:**

```
<?# ShortcodeName "parameter 1" parameter2 "parameter value 3" /?>
```

**A single named parameter and value:**

```
<?# ShortcodeName Foo=Bar /?>
```

**A single named parameter and quoted value:**

```
<?# ShortcodeName Foo="Bar Baz" /?>
```

**A mixture of unnamed positional parameter values and named parameters:**

```
<?# ShortcodeName "unnamed value" Foo=Bar /?>
```

Note that unnamed positional parameters almost always must appear before named parameters.

## Content

passing through with processing instructions

# Writing Shortcodes

## Config File

## As A Class

# Rendering Shortcodes

The `Shortcode` module is used to find shortcodes within a document and render them. In the blog and docs recipes it's applied to every page _after_ templating engines like Markdown and Razor. It's generally an accepted pattern to use the `Shortcode` module after all other templates have been evaluated, but you can certainly use it earlier in your pipelines if you want to.