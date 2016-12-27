Title: SSL and HTTPS
Description: Making your Wyam site work over HTTPS.
---
While getting SSL and HTTPS up and running on your web host and DNS server may have some challenges, Wyam is designed to make HTTPS support easy from a content perspective. All of the built-in themes use local CSS and JavaScript resources, except for Google Fonts which use the `//` addressing scheme for automatic HTTP or HTTPS selection.

Most of the links to other sections of your site will be relative and will use whatever HTTP/HTTPS protocol the rest of your site uses. However, some links such as those used in feeds may have a full URL. To make sure these are generated with the HTTPS protocol, just add the following to your [configuration file](/docs/usage/configuration):

```
Settings.LinksUseHttps = true;
```