Order: 5
Description: Details about the theme that supports this recipe and how to customize them.
---
The Docs recipe only contains a single theme given how complex a documentation site is. It's conceivable that additional themes could be added in the future.

# Overriding Theme Files

One way to customize the output of the recipe is to [override specific theme files](/docs/concepts/themes#overriding-theme-files) with your own versions. Some theme files are even designed specifically for this purpose. While any theme file can be overridden, these are some of the more useful ones you should focus on. To implement an override, just create a new file with the same name in your own input path. In many cases you'll also want to start with the original content of the theme file ([as available in the repository](https://github.com/Wyamio/Wyam/tree/master/themes)) and edit it from there.

- **`/assets/css/bootstrap/variables.less`**
  
  This is the main variables file for Bootstrap. You can use it to adjust colors, fonts, etc. Just be sure that you copy the original file before making any changes so that you maintain all the variables that Bootstrap needs.

- **`/assets/css/override.less`**
  
  You can use this file to define additional CSS styles. You can also define CSS override styles in this file since it's included after the main Bootstrap and theme CSS files.

- **`/assets/img/logo.png`**

  Place your site logo here. The file should be 170 pixels wide and ideally have a transparent background.

- **`/assets/img/favicon.ico`**

  Put your favicon here.

- **`/_Head.cshtml`**

  This is included as part of the `<head>` content of every page after the theme elements. You can use it to add any additional scripts, stylesheets, etc.

- **`/_Navbar.cshtml`**

  You can use this to define a custom navigation bar for the top of your page.

- **`/_ApiBeforeContent.cshtml`**

  This is included prior to the content of each API page. You can use it to display warnings, notices, or other API-specific information that isn't part of the recipe.

- **`/_ApiAfterContent.cshtml`**

  This is included after the content of each API page.

- **`/_Bottom.cshtml`**

  This is included at the bottom of every page. Use it to add JavaScript code such as Google Analytics tracking.

- **`/_Footer.cshtml`**

  This contains the site footer and overriding it will let you specify your own footer.

- **`/index.cshtml`**

  This is the default home page for your site. Even though the theme includes a basic one, you'll probably want to replace it.

- **`/Shared/_Infobar.cshtml`**

  This contains the code for the right-hand side infobar.
