Title: How To Set The CurrentCulture
RedirectFrom: knowledgebase/how-to-set-the-currentculture
---
The best way to do this is to add the following in your [configuration file](/docs/usage/configuration):

```
System.Globalization.CultureInfo.DefaultThreadCurrentCulture
    = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
```

That should set the culture for the primary thread as well as any other threads spawned during the generation process.