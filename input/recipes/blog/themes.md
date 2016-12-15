Order: 6
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

One way to customize the output of the recipe is to [override specific theme files](/docs/concepts/themes#overriding-theme-files) with your own versions. Some theme files are even designed specifically for this purpose. While any theme file can be overridden, these are some of the more useful ones you should focus on. To implement an override, just create a new file with the same name in your own input path. In many cases you'll also want to start with the original content of the theme file ([as available in the repository](https://github.com/Wyamio/Wyam/tree/master/themes)) and edit it from there.

While anyone can create themes for any recipe, the official themes for the this recipe all follow a similar convention and have the following files (unless otherwise indicated).

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
  
- **`/posts/_PostFooter.cshtml`**

  This is included at the bottom of every blog post and can be used for placing extra per-post content like commenting systems (I.e., Disqus).