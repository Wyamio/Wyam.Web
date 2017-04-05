Title: Setting The Culture
RedirectFrom:
  - knowledgebase/how-to-set-the-currentculture
  - docs/advanced/how-to-set-the-currentculture
---

# CurrentCulture

You can set the current culture for the entire generation with the following line in your [configuration file](/docs/usage/configuration):

```
System.Globalization.CultureInfo.DefaultThreadCurrentCulture
    = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
```

That should set the culture for the primary thread as well as any other threads spawned during the generation process.

# Dates

The culture for dates is handled specially since they're used by many of the modules, recipes, and themes. When processing dates, two settings take precedence over the current culture, `DateTimeInputCulture` and `DateTimeDisplayCulture`.

The following logic applies when interpreting an input date or time:

* If a global metadata value for `DateTimeInputCulture` is set, use that.
* Otherwise, use the current culture.

The following logic applies when generating display strings for dates and times:

* If a global metadata value for `DateTimeDisplayCulture` is set, use that.
* Otherwise, if the current culture is English-speaking then use it for date display (the assumption is that the surrounding text in most themes will also be English).
* Otherwise, if the current culture is not English-speaking use "en-GB".

The `DateTimeCultureExtensions` class has extension methods for using the metadata settings to retrieve properly formatted date and time strings. Whenever possible, the extensions in that class should be preferred over direct calls to `DateTime.ToString()` and it's variants.