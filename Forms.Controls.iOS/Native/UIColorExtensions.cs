using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native
{
    public static class UIColorExtensions
    {
        public static UIColor Lighter(this UIColor self, float percentage)
        {
            return self.Adjust(Math.Abs(percentage));
        }

        public static UIColor Darken(this UIColor self, float percentage)
        {
            return self.Adjust(-1 * Math.Abs(percentage));
        }


        public static UIColor Adjust(this UIColor self, float percentage)
        {
            nfloat r, g, b, a;
            self.GetRGBA(out r, out g, out b, out a);

            return new UIColor(
              (float)Math.Min(r + percentage / 100, 1f),
              (float)Math.Min(g + percentage / 100, 1f),
              (float)Math.Min(b + percentage / 100, 1f),
                a);
        }

    }
}
