Title: Blog
Description: A recipe for blog and other sites with dated posts, tags, and feed support.
---
The Blog recipe is probably the best way to get an experience similar to other blog-based static site generators like Jekyll. It will pick up all Markdown and Razor files in the `/posts` directory as blog posts, and all Markdown and Razor files in the root site directory as additional information pages. It will also generate Atom and RSS feeds for your posts, as well as create archives by date and by tags.

To scaffold the recipe, run:

```
wyam new -r Blog
```

To build a site with the recipe, run:

```
wyam -r Blog
```

## Global Metadata and Settings

While it's not required, it's recommended that you create a configuration file to define the `Host` setting and the following global metadata:

* `Title` - The title of the site, used in several places including the web page title.
* `Description` - The description of the site used on the home page.
* `Intro` - A small introduction paragraph used on the home page.

A configuration file that defines these would look something like:

```
Settings.Host = "example.com";
GlobalMetadata["Title"] = "Dave Glick";
GlobalMetadata["Description"] = "The personal blog of Dave Glick";
GlobalMetadata["Intro"] = "Hi, welcome to my blog!";
```

## Document Metadata

Each document and/or post can also define some metadata (usually in front matter).

* `Title` - The title of the page or post.
* `Lead` - A short introduction sentence about the page or post.
* `Image` - A link (relative or absolute) to a large hero image that should be displayed on the page.
* `Published` - The date the post was published.
* `Tags` - A list of tags for the post.
* `ShowInNavbar` - A true/false value indicating whether the page should be shown in the top navigation bar.

The front matter for a post might look something like this:

```
Title: First Post
Lead: This is my first post to my blog.
Image: images/firstpost.jpg
Published: 1/7/2016
Tags:
  - Tag1
  - Tag2
---
The text of the post goes here...
```

# Themes

<div class="row">
<div class="col-md-4">
<h2>CleanBlog</h2>
<img src="/Content/images/themes/blog-cleanblog.png" class="img-responsive img-thumbnail" alt="CleanBlog">

The CleanBlog theme contains a clean layout with a large top banner area.

To build the Blog recipe with the CleanBlog theme, run:

```
wyam -r Blog -t CleanBlog
```
</div>
<div class="col-md-4">
<h2>Phantom</h2>
<img src="/Content/images/themes/blog-phantom.png" class="img-responsive img-thumbnail" alt="CleanBlog">

An airy theme based on the [Phantom](https://html5up.net/phantom) template from [HTML5 Up](https://html5up.net/).

To build the Blog recipe with the Phantom theme, run:

```
wyam -r Blog -t Phantom
```
</div>
<div class="col-md-4">
</div>
</div>