Title: Trophy
Description: A rugged theme that features images front and center.
NuGet: Wyam.Blog.Trophy
Source: https://github.com/Wyamio/Wyam/tree/develop/themes/Blog/Trophy
# Author: Wyam
# AuthorLink: http://wyam.io
# Link: http://www.github.com/asdf
# Preview: http://some.other.preview/ OR false for no preview
# Git: https://github.com/Wyamio/Wyam.git
---
This theme is a port of the Jekyll [Trophy](https://github.com/thomasvaeth/trophy-jekyll) theme.

# Usage Notes

## Adding Disqus

Adding Disqus (or any other commenting system) to your blog posts is easy. You'll need to include the Disqus code on every blog post (but not other pages), so `/_PostFooter.cshtml` is probably the best place to put it. Add the following to a `_PostFooter.cshtml` file in your input folder:

```
<div id="disqus_thread"></div>
<script type="text/javascript">
    /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
    var disqus_shortname = 'your-shortname'; // required: replace example with your forum shortname
    var disqus_identifier = '@Model.FilePath(Keys.RelativeFilePath).FileNameWithoutExtension.FullPath';
    var disqus_title = '@Model.String(BlogKeys.Title)';
    var disqus_url = '@Context.GetLink(Model, true)';

    /* * * DON'T EDIT BELOW THIS LINE * * */
    (function() {
        var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
        dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
    })();
    
    (function () {
        var s = document.createElement('script'); s.async = true;
        s.type = 'text/javascript';
        s.src = '//' + disqus_shortname + '.disqus.com/count.js';
        (document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);
    }());
</script>
<noscript>Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
<a href="http://disqus.com" class="dsq-brlink">comments powered by <span class="logo-disqus">Disqus</span></a>
```

This example uses the file path as the Disqus ID. Depending on how you have your Disqus account set up, you many need to tweak the configuration variables.