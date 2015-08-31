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

  - `ApplyFilters(params ImageFilter[] filters)`
    
    Apply one or more filters to the image. The available filters are as follows:
    
    - ImageFilter.BlackAndWhite
    - ImageFilter.Comic
    - ImageFilter.Gotham
    - ImageFilter.GreyScale
    - ImageFilter.HiSatch
    - ImageFilter.Invert
    - ImageFilter.Lomograph
    - ImageFilter.LoSatch
    - ImageFilter.Polaroid
    - ImageFilter.Sepia

  - `Brigthen(short percentage)`
    
    Brigthen image using percentage from 0 (no processing) to 100%.
    
  - `Darken(short percentage)`
  
    Darken the iamge using percentage from 0 (no processing) to 100%.
    
  - `SetOpacity(short percentage)`
  
    Set opacity of the image from 0 to 100%.
    
  - `SetHue (short degrees, bool rotate = false)`
  
    Set the hue of the image using 0 to 360 degree values. 
    
  - `Tint(Color color)`
  
    Tint the image with specific color.
    
  - `Vignette(Color color)`
  
    Apply Vignette processing to the image with specific color.
    
  - `Saturate(short percentage)`
  
    Saturate the image from 0 to 100%.
    
  - `Desaturate(short percentage)`
  
    Desaturate the image from 0 to 100%.
    
  - `SetContrast(short percentage)`
  
    Set the contrast value of the image from the value of -100 to 100.
    
  - `SetSuffix(string suffix)`
   
    Set the suffix of the generated processed image.
    
  - `SetPrefix(string prefix)`
  
    Set the prefix of the generated process image.
    
  - `And`
  
    Begins another set of processing instructions to be applied to the image. 