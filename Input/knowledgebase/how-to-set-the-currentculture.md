Title: How To Set The CurrentCulture
---
The best way to do this is to add the following in the [main body](/getting-started/configuration#body) of your configuration file:

```
System.Globalization.CultureInfo.DefaultThreadCurrentCulture
    = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
```

That should set the culture for the primary thread as well as any other threads spawned during the generation process.