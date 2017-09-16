using System;

namespace Messier16.Forms.iOS.Controls
{
    public static class Messier16Controls
    {
        public static void InitAll()
        {
            CheckboxRenderer.Init();
            RatingBarRenderer.Init();
            SegmentedControlRenderer.Init();
        }
    }
}

