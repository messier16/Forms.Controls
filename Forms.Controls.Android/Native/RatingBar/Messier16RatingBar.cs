using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;

namespace Messier16.Forms.Android.Controls.Native.RatingBar
{

    public class Messier16RatingBar : View
    {

        private int _maxStar = 5;
        public int MaxStars
        {
            get { return _maxStar; }
            set { _maxStar = value; }
        }

        private float _padding = 2;
        public float Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }

        public Messier16RatingBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitRatingBar();
        }

        public Messier16RatingBar(Context context) : base(context)
        {
            InitRatingBar();
        }


        private Paint fillPaint;
        private Paint strokePaint;


        private void InitRatingBar()
        {
            fillPaint = new Paint();
            fillPaint.Color = new Color(0, 0, 0);

            strokePaint = new Paint();
            strokePaint.StrokeWidth = 2;
            strokePaint.AntiAlias = true;
            strokePaint.Color = new Color(30, 30, 30);
            strokePaint.SetStyle(Paint.Style.Stroke);
            SetPadding(3, 3, 3, 3);
        }

        public Color _fillColor;
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                if (fillPaint != null)
                    fillPaint.Color = value;
                Invalidate();
            }
        }

        public Color _strokeColor;
        public Color StrokeColor
        {
            get { return _strokeColor; }
            set
            {
                _strokeColor = value;
                if (strokePaint != null)
                    strokePaint.Color = value;
                Invalidate();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            SetMeasuredDimension(MeasureWidth(widthMeasureSpec), MeasureHeight(heightMeasureSpec));
        }

        private int MeasureWidth(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            int specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
            {
                result = specSize;
            }
            return result;
        }

        private int MeasureHeight(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            int specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
            {
                result = specSize;
            }
            return result;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            float starSizeWidth = canvas.Width / MaxStars;
            float starSizeHeight = canvas.Height;

            var trueStarSize = Math.Min(starSizeHeight, starSizeWidth);

            var starCenter = starSizeWidth / (float)2;

            var rating = Rating;
            for (int i = 0; i < MaxStars; i++)
            {
                var middle = starSizeWidth * i + starSizeWidth / 2;
                var left = middle - trueStarSize / 2;

                var boundingBox = new RectF(left + Padding, 0, left + trueStarSize - Padding, trueStarSize - Padding);
                canvas.Save();
                var starPath = StarPath(canvas, boundingBox);
                canvas.DrawPath(starPath, strokePaint);
                canvas.ClipPath(starPath);

                RectF fillBox = null;
                if ((i + 1) < Rating)
                {
                    fillBox = new RectF(left, 0, left + trueStarSize, trueStarSize);
                }
                else if (0 < Rating - i)
                {
                    fillBox = new RectF(left, 0, left + trueStarSize * (Rating - i), trueStarSize);
                }
                if (fillBox != null)
                    canvas.DrawRect(fillBox, fillPaint);
                canvas.Restore();
            }
        }

        Path StarPath(Canvas canvas, RectF frame)
        {

            Path bezierPath = new Path();
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

        private const float TOUCH_SCALE_FACTOR = 180.0f / 320;
        public override bool OnTouchEvent(MotionEvent e)
        {
            float x = e.GetX();
            float y = e.GetY();
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


        float rating = -1;
        public float Rating
        {
            set
            {
                if (value != rating)
                {
                    rating = value;
                    RatingChanged?.Invoke(this, rating);
                    Invalidate();
                }
            }
            get
            {
                return rating;
            }
        }

        void CalculateNewRating(float movement)
        {
            Rating = (float)Math.Ceiling(MaxStars * (movement / Width));
        }
    }
}