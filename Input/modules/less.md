Title: Less
Description: Compiles Less CSS files to CSS stylesheets.
---
Compiles Less CSS files to CSS stylesheets.

Some Less CSS files aren't intended to be output directly such as those that contain mixins or variables. For this reason, the [default configuration](/getting-started/defaults) does not search for all `*.less` files and compile/output them. You will need to create a [custom configuration file](/getting-started/configuration) that indicates exactly which Less CSS files should be compiled and output. For example, here is a pipeline that compiles two Less CSS files, one for Bootstrap (which contains a lot of includes) and a second for custom CSS:

This module uses [DotLess](http://www.dotlesscss.org/).

```
Pipelines.Add("Less",
  ReadFiles("master.less"),
  Concat(ReadFiles("bootstrap.less")),
  Less(),
  WriteFiles(".css")
);
```

# Usage
---
  - `Less()`
  
    The content of the input document is compiled to CSS and the content of the output document contains the compiled CSS stylesheet.