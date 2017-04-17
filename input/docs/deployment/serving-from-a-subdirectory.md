Title: Serving From A Subdirectory
Description: How to support hosts that place your site under a subdirectory.
---
Some hosts place your web site under a subdirectory on the server. For example, while GitHub Pages puts user and organization sites at a root "http://username.github.io" URL, it serves project sites as "http://username.github.io/repository". It's easy to support these kinds of deployments with a couple settings in Wyam.

To make all generated links use the subdirectory name, place the following at the top of your [configuration file](/docs/usage/configuration) and set the appropriate subdirectory:

```
Settings[Keys.LinkRoot] = "/subdirectory";
```

That will result in all the links that were created using `IExecutionContext.GetLink()` having the correct subdirectory. While the [recipes and themes](/recipes) are designed to automatically support this convention, you may need to make changes to your own files or custom generation logic to take advantage of the setting. Make sure that all of the links you specify without using the `GetLink()` method point to the correct relative URL with the subdirectory.

To test how the site will work when a host is serving it under a subdirectory, use the `--virtual-dir "/subdirectory"` option when running via the [command line](/docs/usage/command-line). This will tell the preview web server to serve your site under the specified virtual directory.