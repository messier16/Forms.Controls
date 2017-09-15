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

namespace Messier16.Forms.Android.Controls
{
    public class Messier16Controls
    {
        public static void InitAll()
        {
            SegmentedControlRenderer.Init();
            CheckboxRenderer.Init();
            RatingBarRenderer.Init();
        }
    }
}