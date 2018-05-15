
# Forms.Controls
Home of all Messier16 Xamarin.Forms controls 😁 (now .NET standarized<sup>1</sup>)

Be aware that some functionality might have changed from the previous versions to the 2.X.X (the NET Standard one), if you find any errors (or something that used to work on your previous version but it doesn't anymore), please, submit an [issue](https://github.com/messier16/Forms.Controls/issues/new).

See more Xamarin related posts here: [https://thatcsharpguy.com](https://thatcsharpguy.com/tag/Xamarin/)

## Installing this thing

Get it on NuGet: [Messier16.Forms.Controls](https://www.nuget.org/packages/Messier16.Forms.Controls)

## Using this thing

Make sure you call (as standard with most plugins) the following line in each and every of your client projects:

```
Messier16Controls.InitAll();
```

### Per platform nuances  
There are some specific things that you must take into account when working with the controls, find the details below.

#### Forms.Controls.iOS
##### For the RatingBar to work on iOS:
- You must provide a pair of images as image resources, you'll need one to represent an empty rating state and a filled rating state. I have provided a couple of them ([star.png](https://github.com/messier16/Forms.Controls/blob/net-standard/Sample/SampleNuGetApp/SampleNuGetApp.iOS/Resources/star%403x.png) and [star_filled.png](https://github.com/messier16/Forms.Controls/blob/net-standard/Sample/SampleNuGetApp/SampleNuGetApp.iOS/Resources/star_filled%403x.png))  but I strongly suggest you provide your own images to tailor the experience in your app.
- You **must set** the properties `FilledImage` and `Image` of the `RatingBar` object, to point to the previously mentioned files:
```
ratingBar1.FilledImage = "star_filled.png";
ratingBar1.Image = "star.png";
```

<sup>1</sup>It probably will give you a horrible warning, but it should work without issues. 
