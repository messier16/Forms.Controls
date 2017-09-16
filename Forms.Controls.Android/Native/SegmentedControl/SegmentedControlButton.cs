using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;

namespace Messier16.Forms.Android.Controls.Native.SegmentedControl
{
    public class SegmentedControlButton : RadioButton
    {
        private int _lineHeightSelected;
        private int _lineHeightUnselected;

        private Paint _linePaint;

        public SegmentedControlButton(Context context) : this(context, null)
        {
        }

        public SegmentedControlButton(Context context, IAttributeSet attributes) : this(context, attributes,
            Resource.Attribute.segmentedControlOptionStyle)
        {
        }

        public SegmentedControlButton(Context context, IAttributeSet attributes, int defStyle) : base(context,
            attributes, defStyle)
        {
            Initialize(attributes, defStyle);
        }

        private void Initialize(IAttributeSet attributes, int defStyle)
        {
            var a = Context.ObtainStyledAttributes(attributes, Resource.Styleable.SegmentedControlOption, defStyle,
                Resource.Style.SegmentedControlOption);

            var lineColor = a.GetColor(Resource.Styleable.SegmentedControlOption_lineColor, 0);
            _linePaint = new Paint {Color = lineColor};

            _lineHeightUnselected =
                a.GetDimensionPixelSize(Resource.Styleable.SegmentedControlOption_lineHeightUnselected, 0);
            _lineHeightSelected =
                a.GetDimensionPixelSize(Resource.Styleable.SegmentedControlOption_lineHeightSelected, 0);

            a.Recycle();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (_linePaint.Color != 0 && (_lineHeightSelected > 0 || _lineHeightUnselected > 0))
            {
                var lineHeight = Checked ? _lineHeightSelected : _lineHeightUnselected;

                if (lineHeight > 0)
                {
                    var rect = new Rect(0, Height - lineHeight, Width, Height);
                    canvas.DrawRect(rect, _linePaint);
                }
            }
        }
    }
}