using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace Messier16.Forms.Android.Controls.Native.RatingBar
{
    public class Messier16RatingBar : View
    {
        private const float TouchScaleFactor = 180.0f / 320;

        private Color _fillColor;

        private Color _strokeColor;


        private Paint _fillPaint;


        private float _rating = -1;
        private Paint _strokePaint;

        public Messier16RatingBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitRatingBar();
        }

        public Messier16RatingBar(Context context) : base(context)
        {
            InitRatingBar();
        }

        public int MaxStars { get; set; } = 5;

        public float Padding { get; set; } = 2;

        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                if (_fillPaint != null)
                    _fillPaint.Color = value;
                Invalidate();
            }
        }

        public Color StrokeColor
        {
            get => _strokeColor;
            set
            {
                _strokeColor = value;
                if (_strokePaint != null)
                    _strokePaint.Color = value;
                Invalidate();
            }
        }

        public float Rating
        {
            set
            {
                if (value != _rating)
                {
                    _rating = value;
                    RatingChanged?.Invoke(this, _rating);
                    Invalidate();
                }
            }
            get => _rating;
        }


        private void InitRatingBar()
        {
            _fillPaint = new Paint {Color = new Color(0, 0, 0)};

            _strokePaint = new Paint
            {
                StrokeWidth = 2,
                AntiAlias = true,
                Color = new Color(30, 30, 30)
            };
            _strokePaint.SetStyle(Paint.Style.Stroke);
            SetPadding(3, 3, 3, 3);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            SetMeasuredDimension(MeasureWidth(widthMeasureSpec), MeasureHeight(heightMeasureSpec));
        }

        private int MeasureWidth(int measureSpec)
        {
            var result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            var specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
                result = specSize;
            return result;
        }

        private int MeasureHeight(int measureSpec)
        {
            var result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            var specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
                result = specSize;
            return result;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            float starSizeWidth = canvas.Width / MaxStars;
            float starSizeHeight = canvas.Height;

            var trueStarSize = Math.Min(starSizeHeight, starSizeWidth);

            var starCenter = starSizeWidth / 2;

            var rating = Rating;
            for (var i = 0; i < MaxStars; i++)
            {
                var middle = starSizeWidth * i + starSizeWidth / 2;
                var left = middle - trueStarSize / 2;

                var boundingBox = new RectF(left + Padding, 0, left + trueStarSize - Padding, trueStarSize - Padding);
                canvas.Save();
                var starPath = StarPath(canvas, boundingBox);
                canvas.DrawPath(starPath, _strokePaint);
                canvas.ClipPath(starPath);

                RectF fillBox = null;
                if (i + 1 < Rating)
                    fillBox = new RectF(left, 0, left + trueStarSize, trueStarSize);
                else if (0 < Rating - i)
                    fillBox = new RectF(left, 0, left + trueStarSize * (Rating - i), trueStarSize);
                if (fillBox != null)
                    canvas.DrawRect(fillBox, _fillPaint);
                canvas.Restore();
            }
        }

        private Path StarPath(Canvas canvas, RectF frame)
        {
            var bezierPath = new Path();
            bezierPath.MoveTo(frame.Left + frame.Width(), frame.Top + frame.Height() * 0.39964f);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.65f, frame.Top + frame.Height() * 0.34969f);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.5f, frame.Top);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.35f, frame.Top + frame.Height() * 0.34969f);
            bezierPath.LineTo(frame.Left, frame.Top + frame.Height() * 0.39964f);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.25f, frame.Top + frame.Height() * 0.64942f);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.2f, frame.Top + frame.Height());
            bezierPath.LineTo(frame.Left + frame.Width() * 0.5f, frame.Top + frame.Height() * 0.84924f);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.8f, frame.Top + frame.Height() * 0.9991f);
            bezierPath.LineTo(frame.Left + frame.Width() * 0.75f, frame.Top + frame.Height() * 0.64942f);
            bezierPath.LineTo(frame.Left + frame.Width(), frame.Top + frame.Height() * 0.39964f);
            bezierPath.Close();
            return bezierPath;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var x = e.GetX();
            var y = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    CalculateNewRating(x);
                    break;
                case MotionEventActions.Move:
                    CalculateNewRating(x);
                    break;
                default:
                    break;
            }
            return true;
        }

        public event EventHandler<float> RatingChanged;

        private void CalculateNewRating(float movement)
        {
            Rating = (float) Math.Ceiling(MaxStars * (movement / Width));
        }
    }
}