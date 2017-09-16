using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native.RatingBar
{
    public class EDStarRating : UIControl
    {
        private const float EdDefaultHalfstarThreshold = 0.6f;

        public UIImage TintedStarHighlightedImage { get; private set; }

        public UIImage TintedStarImage { get; private set; }

        public UIImage BackgroundImage { get; set; }

        public UIImage StarHighlightedImage
        {
            get { return _starHighlightedImage; }
            set { SetStarHighlightedImage(value); }
        }

        public UIImage StarImage
        {
            get { return _starImage; }
            set { SetStarImage(value); }
        }

        public int MaxRating { get; set; }
        private float _rating;
        public nfloat HorizontalMargin { get; set; }

        public bool IsEditable { get; set; }
        private StarRatingDisplayMode _displayMode;
        private UIImage _starHighlightedImage;
        private UIImage _starImage;
        public float HalfStarThreshold { get; set; }


        public event RatingChangedEvent RatingChanged;
        //@property(nonatomic, copy) EDStarRatingReturnBlock returnBlock;
        #region Init & dealloc

        public EDStarRating(NSCoder coder) : base(coder)
        {
            SetDefaultProperties();
        }

        public EDStarRating(CGRect frame) : base(frame)
        {
            SetDefaultProperties();
        }

        private void SetDefaultProperties()
        {
            MaxRating = 5;
            _rating = 0;
            _displayMode = StarRatingDisplayMode.Full;
            HalfStarThreshold = EdDefaultHalfstarThreshold;
            HorizontalMargin = 10;
            BackgroundColor = UIColor.Clear;
        }

        #endregion

        #region Setters

        public float Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                SetNeedsDisplay();
                RatingChanged?.Invoke(this, new GenericEventArgs<float>(Rating));
            }
        }

        public StarRatingDisplayMode DisplayMode
        {
            get { return _displayMode; }
            set
            {
                _displayMode = value;
                SetNeedsDisplay();
            }
        }

        #endregion

        #region Touch Interaction

        float StarsForPoint(CGPoint point)
        {
            var stars = 0f;
            for (int i = 0; i < MaxRating; i++)
            {
                CGPoint p = PointOfStarAtPosition(i, false);
                if (point.X > p.X)
                {
                    var increment = (float)1.0f;

                    if (DisplayMode == StarRatingDisplayMode.Half)
                    {
                        float difference = (float)((point.X - p.X) / StarImage.Size.Width);
                        if (difference < HalfStarThreshold)
                        {
                            increment = 0.5f;
                        }
                    }
                    stars += increment;
                }
            }
            return stars;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            if (!IsEditable)
                return;

            var touch = touches.AnyObject as UITouch;
            var touchLocation = touch.LocationInView(this);
            Rating = StarsForPoint(touchLocation);
            SetNeedsDisplay();
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            if (!IsEditable)
                return;
            var touch = touches.AnyObject as UITouch;
            var touchLocation = touch.LocationInView(this);
            Rating = StarsForPoint(touchLocation);
            SetNeedsDisplay();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            if (!IsEditable)
                return;
        }

        #endregion

        #region Drawing

        CGPoint PointOfStarAtPosition(int position, bool highlighted)
        {
            var size = highlighted ? StarHighlightedImage.Size : StarImage.Size;

            var starsSpace = Bounds.Size.Width - 2 * HorizontalMargin;

            var interSpace = MaxRating - 1 > 0 ? (starsSpace - (MaxRating) * size.Width) / (MaxRating - 1) : 0;

            if (interSpace < 0)
            {
                interSpace = 0;
            }

            nfloat x = HorizontalMargin + size.Width * position;

            if (position > 0)
            {
                x += interSpace * position;
            }

            nfloat y = (Bounds.Size.Height - size.Height) / ((nfloat)2.0);

            return new CGPoint(x, y);
        }

        void DrawBackgroundImage()
        {
            BackgroundImage?.Draw(Bounds);
        }

        void DrawImage(UIImage image, int position)
        {
            var point = PointOfStarAtPosition(position, true);
            image?.Draw(point);
        }

        CGColor CoreGraphicsColor(UIColor color)
        {
            return color.CGColor;
        }

        CGContext CurrentContext()
        {
            return UIGraphics.GetCurrentContext();
        }

        public override void Draw(CGRect rect)
        {
            var bounds = Bounds;
            var ctx = CurrentContext();

            // Fill background color
            ctx.SetFillColor(BackgroundColor.CGColor);
            ctx.FillRect(bounds);

            DrawBackgroundImage();

            // Draw rating images
            var starSize = StarHighlightedImage.Size;
            for (int i = 0; i < MaxRating; i++)
            {
                DrawImage(TintedStarImage, i);
                if (i < _rating) // Draws over the previous drawing
                {
                    ctx.SaveState();
                    {
                        if (i < _rating && _rating < i + 1)
                        {

                            var starPoint = PointOfStarAtPosition(i, false);
                            float difference = _rating - i;
                            var rectClip = new CGRect
                            {
                                X = starPoint.X,
                                Y = starPoint.Y,
                                Size = starSize
                            };

                            if (DisplayMode == StarRatingDisplayMode.Half && difference < HalfStarThreshold)    // Draw half star image
                            {
                                var size = rectClip.Size;
                                size.Width = size.Width / 2;
                                rectClip.Size = size;
                            }
                            else if (DisplayMode == StarRatingDisplayMode.Half)
                            {
                                var size = rectClip.Size;
                                size.Width = size.Width * difference;
                                rectClip.Size = size;
                            }
                            else
                            {
                                var size = rectClip.Size;
                                size.Width = 0;
                                rectClip.Size = size;
                            }
                            if (rectClip.Size.Width > 0)
                                ctx.ClipToRect(rectClip);

                        }
                        DrawImage(TintedStarHighlightedImage, i);
                    }
                    ctx.RestoreState();
                }
            }

            base.Draw(rect);
        }

        #endregion

        #region Tint color support
        public void SetStarImage(UIImage image)
        {
            if (Equals(StarImage, image))
                return;

            _starImage = image;
            TintedStarImage = GetTintedImage(image);
        }

        public void SetStarHighlightedImage(UIImage image)
        {
            if (Equals(StarHighlightedImage, image))
                return;

            _starHighlightedImage = image;
            TintedStarHighlightedImage = GetTintedImage(image);

        }
        UIImage GetTintedImage(UIImage img)
        {
            //// Make sure tintColor is available (>= iOS 7.0 runtime)
            //if( [self respondsToSelector:@selector(tintColor)] && img.renderingMode == UIImageRenderingModeAlwaysTemplate )
            //{
            UIGraphics.BeginImageContextWithOptions(img.Size, false, UIScreen.MainScreen.Scale);
            var context = UIGraphics.GetCurrentContext();
            context.TranslateCTM(0, img.Size.Height);
            context.ScaleCTM((nfloat)1.0, (nfloat)(-1.0));
            var rect = new CGRect(0, 0, img.Size.Width, img.Size.Height);
            // draw alpha-mask
            context.SetBlendMode(CGBlendMode.Normal);
            context.DrawImage(rect, img.CGImage);
            // draw tint color, preserving alpha values of original image
            context.SetBlendMode(CGBlendMode.SourceIn);
            TintColor.SetFill();
            context.FillRect(rect);

            var tintedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            //}
            return tintedImage;
        }

        public override void TintColorDidChange()
        {
            TintedStarHighlightedImage = GetTintedImage(StarHighlightedImage);
            TintedStarImage = GetTintedImage(StarImage);
            base.TintColorDidChange();
        }
        #endregion

        public delegate void RatingChangedEvent(object sender, GenericEventArgs<float> args);
    }

}
