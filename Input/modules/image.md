Title: Image
Description: Manipulates images.
---
This module manipulates images by applying operations such as resizing, darken/lighten, etc. The module allows you to perform more than one set of processing instructions by using the fluent property `and`.

This module uses [ImageProcessor](http://imageprocessor.org/)


# Usage
---

  - `Image()`
  
    Create the initial module. 
    
    
# Fluent Methods

Chain these methods together after constructor to start processing images. 

  - `Resize(int? width, int? height, AnchorPosition anchor = AnchorPosition.Center)`
  
    Resize image to a certain width and height. It will crop the image whenever necessary.
    
    If `width` is set to `null` or `0`, the image will be resized to its height.
    
    If `height` is set to `null` or `0`,  the image will be resized to its width.
    
    If both `width` and `height` are specified and set to larger than 0, the image will perform cropping if necessary. The cropping position is determined by `anchor`.
    
    The module will generate error if both `width` and `height` are set to null or zero.
    
    If the source image is smaller than the specified `width` and `height`, the image will be enlarged.
 
  - `Constrain(int width, int height)`
 
    If the image is larger than the specified `width` and `height`, it will be resized down.
    
    If the image is smaller than the specified `width` and `height`, it will be left alone. 

