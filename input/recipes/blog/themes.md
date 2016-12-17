Order: 6
Description: Details about the themes that support this recipe and how to customize them.
---
<div class="row">
  <div class="col-sm-6 col-md-4">
    <div class="thumbnail">
      <img src="/assets/img/themes/blog-cleanblog.png" alt="CleanBlog">
      <div class="caption">
        <h3>CleanBlog</h3>
        <p>A clean layout with a large top banner area.</p>
      </div>
    </div>
  </div>
  <div class="col-sm-6 col-md-4">
    <div class="thumbnail">
      <img src="/assets/img/themes/blog-phantom.png" alt="Phantom">
      <div class="caption">
        <h3>Phantom</h3>
        <p>An airy theme based on the <a href="https://html5up.net/phantom">Phantom</a> template from <a href="https://html5up.net/">HTML5 Up</a>.</p>
      </div>
    </div>
  </div>
</div>

# Overriding Theme Files

One way to customize the output of the recipe is to [override specific theme files](/docs/concepts/themes#overriding-theme-files) with your own versions. Some theme files are even designed specifically for this purpose. While any theme file can be overridden, these are some of the more useful ones you should focus on. To implement an override, just create a new file with the same name in your own input path. In many cases you'll also want to start with the original content of the theme file ([as available in the repository](https://github.com/Wyamio/Wyam/tree/master/themes)) and edit it from there.

While anyone can create themes for any recipe, the official themes for the this recipe all follow a similar convention and have the following files (unless otherwise indicated).

- **`/assets/css/override.css`**
  
  You can use this file to define additional CSS styles. You can also define CSS override styles in this file since it's included after the main Bootstrap and theme CSS files.

- **`/favicon.ico`**

  Put your favicon here.

- **`/_Navbar.cshtml`**

  You can use this to define a custom navigation bar for the top of your page.

- **`/_Scripts.cshtml`**

  This is included at the bottom of every page. Use it to add JavaScript code such as Google Analytics tracking.

- **`/_Header.cshtml`**

  Replace this to customize the header for each page.

- **`/_Footer.cshtml`**

  This contains the site footer and overriding it will let you specify your own footer.

- **`/_Sidebar.cshtml`**

  Additional content that gets displayed on the right-hand sidebar on your homepage.
  
- **`/_PostFooter.cshtml`**

  This is included at the bottom of every blog post and can be used for placing extra per-post content like commenting systems (I.e., Disqus).

# Usage Notes

## Adding Disqus

Adding Disqus (or any other commenting system) to your blog posts is easy. You'll need to include the Disqus code on every blog post (but not other pages), so `/_PostFooter.cshtml` is probably the best place to put it. Just add the following to a `_PostFooter.cshtml` file in your input folder:

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