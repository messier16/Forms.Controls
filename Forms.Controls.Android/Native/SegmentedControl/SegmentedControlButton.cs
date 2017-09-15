using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;

namespace Messier16.Forms.Android.Controls.Native.SegmentedControl
{

    public class SegmentedControlButton : RadioButton
    {
        private int lineHeightSelected;
        private int lineHeightUnselected;

        private Paint linePaint;

        public SegmentedControlButton(Context context) : this(context, null)
        {
        }

        public SegmentedControlButton(Context context, IAttributeSet attributes) : this(context, attributes, Resource.Attribute.segmentedControlOptionStyle)
        {
        }

        public SegmentedControlButton(Context context, IAttributeSet attributes, int defStyle) : base(context, attributes, defStyle)
        {
            Initialize(attributes, defStyle);
        }

        private void Initialize(IAttributeSet attributes, int defStyle)
        {
            var a = this.Context.ObtainStyledAttributes(attributes, Resource.Styleable.SegmentedControlOption, defStyle, Resource.Style.SegmentedControlOption);

            var lineColor = a.GetColor(Resource.Styleable.SegmentedControlOption_lineColor, 0);
            linePaint = new Paint();
            linePaint.Color = lineColor;

            lineHeightUnselected = a.GetDimensionPixelSize(Resource.Styleable.SegmentedControlOption_lineHeightUnselected, 0);
            lineHeightSelected = a.GetDimensionPixelSize(Resource.Styleable.SegmentedControlOption_lineHeightSelected, 0);

            a.Recycle();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (linePaint.Color != 0 && (lineHeightSelected > 0 || lineHeightUnselected > 0))
            {
                var lineHeight = Checked ? lineHeightSelected : lineHeightUnselected;

                if (lineHeight > 0)
                {
                    var rect = new Rect(0, Height - lineHeight, Width, Height);
                    canvas.DrawRect(rect, linePaint);
                }
            }
        }
    }

}
