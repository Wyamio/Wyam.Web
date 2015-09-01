Title: Image
Description: Manipulates images.
Category: Data Processing
---
This module manipulates images by applying operations such as resizing, darken/lighten, etc. This module uses [ImageProcessor](http://imageprocessor.org/).

This image module does not modify your original images in anyway. It will create a copy of your images and produce images in the same image format as the original.

It relies on other modules such as `ReadFiles` to read the actual images as input and `WriteFiles` to write images to disk.

```
Pipelines.Add("Images",
    ReadFiles("*").Where(x => x.Contains("images\\") && new[] { ".jpg", ".jpeg", ".gif", ".png"}.Contains(Path.GetExtension(x))),
    Image()
      .SetJpegQuality(100).Resize(400,209).SetSuffix("-thumb"),
    WriteFiles("*")
);
```

It will produce image with similar file name as the original image with addition of suffix indicating operations that have performed, e.g. `hello-world.jpg` can result in `hello-world-w100.jpg`.

The module allows you to perform more than one set of processing instructions by using the fluent property `and`.

```
Pipelines.Add("Images",
    ReadFiles("*").Where(x => x.Contains("images\\") && new[] { ".jpg", ".jpeg", ".gif", ".png"}.Contains(Path.GetExtension(x))),
    Image()
      .SetJpegQuality(100).Resize(400,209).SetSuffix("-thumb")
      .And
      .SetJpegQuality(70).Resize(400*2, 209*2).SetSuffix("-medium"),
    WriteFiles("*")
);
```

Above configuration produces two set of new images, one with `-thumb` suffix and the other with `-medium` suffix.

# Usage
---

  - `Image()`
  
    Create the module. 
    
    
# Fluent Methods

Chain these methods together after constructor to start processing images. 

  - `Resize(int? width, int? height, AnchorPosition anchor = AnchorPosition.Center)`
  
    Resize image to a certain width and height. It will crop the image whenever necessary.
    
    If `width` is set to `null` or `0`, the image will be resized to its height.
    
    If `height` is set to `null` or `0`,  the image will be resized to its width.
    
    If both `width` and `height` are specified and set to larger than 0, the image will perform cropping if necessary. The cropping position is determined by `anchor` with the possible values of `AnchorPosition[Center|Top|Bottom|Left|Right|TopLeft|TopRight|BottomLeft|BottomRight]`.
    
    The module will not perform any image resizing if both `width` and `height` are set to null.
    
    If the source image is smaller than the specified `width` and `height`, the image will be enlarged.
 
  - `Constrain(int width, int height)`
 
    If the image is larger than the specified `width` and `height`, it will be resized down.
    
    If the image is smaller than the specified `width` and `height`, it will not be resized. 

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
    
    These filter values map directly to filters provided by ImageProcessor library. You can see the effects of these filters [here](http://imageprocessor.org/imageprocessor/imagefactory/filter/).

  - `Brigthen(short percentage)`
    
    Brigthen image using percentage from 0 (no processing) to 100.
    
  - `Darken(short percentage)`
  
    Darken the image using percentage from 0 (no processing) to 100.
    
  - `SetOpacity(short percentage)`
  
    Set opacity of the image from 0 to 100.
    
  - `SetHue (short degrees, bool rotate = false)`
  
    Set the hue of the image using 0 to 360 degree values. 
    
  - `Tint(Color color)`
  
    Tint the image with specific color, e.g. `SetTint(Color.Aqua)`. Please check [here](https://msdn.microsoft.com/en-us/library/system.drawing.color(v=vs.110).aspx) for more color values.
    
  - `Vignette(Color color)`
  
    Apply Vignette processing to the image with specific color, e.g. `Vignette(Color.AliceBlue)`. Please check [here](https://msdn.microsoft.com/en-us/library/system.drawing.color(v=vs.110).aspx) for more color values.
    
  - `Saturate(short percentage)`
  
    Saturate the image from 0 to 100.
    
  - `Desaturate(short percentage)`
  
    Desaturate the image from 0 to 100.
    
  - `SetContrast(short percentage)`
  
    Set the contrast value of the image from the value of -100 to 100.
    
  - `SetSuffix(string suffix)`
   
    Set the suffix of the generated image, e.g. `SetSuffix("-medium")` will transform original filename `hello-world.jpg` to `hello-world-medium.jpg`.
    
  - `SetPrefix(string prefix)`
  
    Set the prefix of the generated image, e.g. `SetPrefix("medium-")` will transform original filename `hello-world.jpg` to `medium-hello-world.jpg`.
    
  - `SetJpegQuality(short value)`
  
    This setting only applies to JPEG images. It sets the quality of the JPEG output. The possible values are from 0 to 100.
      
  - `And`
  
    Mark the beginning of another set of processing instructions to be applied to the images. 
