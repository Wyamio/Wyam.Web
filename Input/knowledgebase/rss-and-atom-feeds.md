Title: RSS and Atom Feeds
Description: How to create an RSS and/or Atom feed.
---
RSS and Atom are both syndication formats designed to publish updates to your website to clients such as feed readers. Most blog platforms have support for one or both, and like many things in Wyam there are multiple ways you can add support for RSS and/or Atom to your own site.

# SyndicationFeed
---

Perhaps the easiest method is to use the [`SyndicationFeed`](https://msdn.microsoft.com/en-us/library/system.servicemodel.syndication.syndicationfeed.aspx) class provided by WCF. It allows you to programmatically build either type of feed and then output it as a string. To get started, you'll need to make sure you include the `System.ServiceModel` assembly in your configuration file (which must be loaded by full name since it's in the GAC):

```
Assemblies
    .Load("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
```

Then, create a Razor page and add code similar to the following (this is the actual feed code from [my blog](http://daveaglick.com)):

```
WriteFileName: feed.rss
---
@using System.ServiceModel.Syndication; 
@using System.IO;
@using System.Xml;
@using AngleSharp;
@using AngleSharp.Parser.Html;

@{
	Layout = string.Empty;
	
	Uri baseUri = new Uri(@"http://daveaglick.com");  // This is the root URL to your site
    SyndicationFeed feed = new SyndicationFeed()
    {
        Title = new TextSyndicationContent("Dave Glick"),  // This should be the name of your site or feed
        Description = new TextSyndicationContent("Latest blog posts by Dave Glick"),  // This is a short description of your site or feed
        BaseUri = baseUri,
        Items = Documents
            .Where(x => x.ContainsKey("Published"))  // This should be the metadata field you use for storing item dates (make sure to replace it everywhere)
            .OrderByDescending(x => x.Get<DateTime>("Published"))
            .Take(10)  // This is the number of recent items you want in your feed
            .Select(x => {             
                Uri uri = new Uri(baseUri, "/" + PathHelper.RemoveExtension(x.String("RelativeFilePath")));  // This is the URL for the feed item
                SyndicationItem item = new SyndicationItem(
                    x.String("Title") + (x.ContainsKey("Lead") ? " - " + x.String("Lead") : string.Empty),  // This is the title of the feed item
                    new HtmlParser(x.Content).Parse().QuerySelector("div#post-content").InnerHtml,  // This uses AngleSharp to get the div with an id of "post-content"
                    uri, uri.ToString(), x.Get<DateTime>("Published"))
                {
                    PublishDate = x.Get<DateTime>("Published")
                };

                item.Authors.Add(new SyndicationPerson("", "Dave Glick", ""));  // These are the authors for your feed item
                return item;
            })
    };
    feed.Links.Add(new SyndicationLink(feed.BaseUri));
    using (XmlTextWriter writer = new XmlTextWriter(Output))
    {
        new Rss20FeedFormatter(feed).WriteTo(writer);
    }        
}
```

A couple things to note:
  - This uses [YAML](/modules/yaml) [front matter](/modules/frontmatter) to declare the `WriteFileName` metadata which the [WriteFiles](/modules/writefiles) module will use for the file name. GitHub Pages recognizes files with the extension `.rss` and `.atom` and serves them with the correct content type. If you're not using GitHub Pages, you'll need to make sure your web server servers RSS feeds with the content type `application/rss+xml` and Atom feeds with `application/atom+xml`.
  
  - My blog contains a date metadata key `Published`. You should replace references to it in the code above with whatever your site uses to store the post date.
  
  - This code uses [AngleSharp](https://github.com/FlorianRappl/AngleSharp) to find the `div` with an `id` of `post-content` in each item. In the layout file, this `div` contains the `@RenderBody()` call so this code essentially strips out everything but the content. If you want to do the same thing, make sure to include AngleSharp in your configuration file:
  
  ```
  Packages
	.Install("AngleSharp");
  ```
  
  - This code produces an RSS feed using the `Rss20FeedFormatter` class. Just change this to `Atom10FeedFormatter` to output an Atom feed instead.
  
# Directly
---

If you don't want to use `SyndicationFeed`, you can always create your feed from scratch using XML. In this case, you would create a Razor page that contains XML content that follows the feed format specification and then use normal Razor code to iterate your items, etc.
