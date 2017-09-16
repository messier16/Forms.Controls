using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using System.ComponentModel;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox
{
    public class Checkbox : UIControl
    {

        #region Public properties
        private float _checkLineWidth = 2f;
        [Export("CheckLineWidth"), Browsable(true)]
        public float CheckLineWidth
        {
            get { return _checkLineWidth; }
            set { _checkLineWidth = value; LayoutLayers(); }
        }

        private UIColor _checkColor = UIColor.FromRGB(84, 142, 205);
        [Export("CheckColor"), Browsable(true)]
        public UIColor CheckColor
        {
            get { return _checkColor; }
            set { _checkColor = value; ColorLayers(); }
        }

        private float _containerLineWidth = 2f;
        [Export("ContainerLineWidth"), Browsable(true)]
        public float ContainerLineWidth
        {
            get { return _containerLineWidth; }
            set { _containerLineWidth = value; LayoutLayers(); }
        }

        UIColor _checkboxBackgroundColor = UIColor.FromRGB(49, 95, 153);
        [Export("CheckboxBackgroundColor"), Browsable(true)]
        public UIColor CheckboxBackgroundColor
        {
            get { return _checkboxBackgroundColor; }
            set { _checkboxBackgroundColor = value; SetNeedsDisplay(); }
        }

        private bool _fillsOnChecked = true;
        [Export("FillsOnChecked"), Browsable(true)]
        public bool FillsOnChecked
        {
            get { return _fillsOnChecked; }
            set { _fillsOnChecked = value; ColorLayers(); }
        }

        bool _checked;
        [Export("Checked"), Browsable(true)]
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; ColorLayers(); }
        }

        #endregion

        #region Internal and private propertoies
        CAShapeLayer containerLayer = new CAShapeLayer();
        CAShapeLayer checkLayer = new CAShapeLayer();

        CGRect frame
        {
            get
            {
                var width = (float)Bounds.Width;
                var height = (float)Bounds.Height;
				
                float x;
                float y;

                float sideLength;
                if (width > height)
                {
                    sideLength = height;
                    x = (width - sideLength) / 2;
                    y = 0;
                }
                else
                {
                    sideLength = width;
                    x = 0;
                    y = (height - sideLength) / 2;
                }

                var halfLineWidth = ContainerLineWidth / 2;
                return new CGRect(x: x + halfLineWidth, y: y + halfLineWidth, width: sideLength - ContainerLineWidth, height: sideLength - ContainerLineWidth);
            }

        }

        UIBezierPath containerPath => UIBezierPath.FromRoundedRect(new CGRect(frame.GetMinX() + NMath.Floor(frame.Width * 0.05000f) + 0.5f, frame.GetMinY() + NMath.Floor(frame.Height * 0.05000f) + 0.5f, NMath.Floor(frame.Width * 0.95000f) - NMath.Floor(frame.Width * 0.05000f), NMath.Floor(frame.Height * 0.95000f) - NMath.Floor(frame.Height * 0.05000f)), frame.Height * 0.1f);

        UIBezierPath checkPath
        {
            get
            {
                var checkPath = new UIBezierPath();
                checkPath.MoveTo(new CGPoint(frame.GetMinX() + 0.76208f * frame.Width, frame.GetMinY() + 0.26000f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.38805f * frame.Width, frame.GetMinY() + 0.62670f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.23230f * frame.Width, frame.GetMinY() + 0.47400f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.18000f * frame.Width, frame.GetMinY() + 0.52527f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.36190f * frame.Width, frame.GetMinY() + 0.70360f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.38805f * frame.Width, frame.GetMinY() + 0.72813f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.41420f * frame.Width, frame.GetMinY() + 0.70360f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.81437f * frame.Width, frame.GetMinY() + 0.31127f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.76208f * frame.Width, frame.GetMinY() + 0.26000f * frame.Height));
                checkPath.ClosePath();
                return checkPath;
            }
        }

        const float validBoundsOffset = 80f;
        #endregion


        #region Initialization
        public Checkbox(NSCoder coder) : base(coder) { }

        public override void AwakeFromNib()
        {
            CustomInitialization();
        }

        public Checkbox(CGRect frame) : base(frame)
        {
            CustomInitialization();
        }

        public Checkbox(CGRect frame, bool on) : this(frame)
        {
            this.Checked = on;
        }

        void CustomInitialization()
        {
            checkLayer.FillColor = UIColor.Clear.CGColor;
            ColorLayers();
            LayoutLayers();
            Layer.AddSublayer(containerLayer);
            Layer.AddSublayer(checkLayer);
        }
        #endregion

        #region Layout layers
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            LayoutLayers();
        }
        void LayoutLayers()
        {
            // Set frames, line widths and paths for layers
            containerLayer.Frame = Bounds;
            containerLayer.LineWidth = ContainerLineWidth;
            containerLayer.Path = containerPath.CGPath;

            checkLayer.Frame = Bounds;
            checkLayer.LineWidth = CheckLineWidth;
            checkLayer.Path = checkPath.CGPath;
        }
        #endregion

        #region Color layers

        void ColorLayers()
        {
            containerLayer.StrokeColor = CheckboxBackgroundColor.CGColor;

            // Set colors based on 'on' property
            if (Checked)
            {
                containerLayer.FillColor = FillsOnChecked ? CheckboxBackgroundColor.CGColor : UIColor.Clear.CGColor;
                checkLayer.FillColor = CheckColor.CGColor;
                checkLayer.StrokeColor = CheckColor.CGColor;
            }
            else
            {
                containerLayer.FillColor = UIColor.Clear.CGColor;
                checkLayer.FillColor = UIColor.Clear.CGColor;
                checkLayer.StrokeColor = UIColor.Clear.CGColor;
            }
        }

        #endregion

        #region Touch tracking
        public override bool BeginTracking(UITouch uitouch, UIEvent uievent)
        {
            base.BeginTracking(uitouch, uievent);
            return true;
        }

        public override bool ContinueTracking(UITouch uitouch, UIEvent uievent)
        {
            base.ContinueTracking(uitouch, uievent);
            return true;
        }

        public override void EndTracking(UITouch uitouch, UIEvent uievent)
        {
            base.EndTracking(uitouch, uievent);
            var touchLocationInView = uitouch?.LocationInView(this);
            if (touchLocationInView == null)
                return;

            //let offset = type(of: self).validBoundsOffset
            //let validBounds = CGRect(x: bounds.origin.x - offset, y: bounds.origin.y - offset, width: bounds.width + (2 * offset), height: bounds.height + (2 * offset))
            var validBounds = Bounds;

            if (validBounds.Contains(touchLocationInView.Value))
            {
                Checked = !Checked;
                SendActionForControlEvents(UIControlEvent.ValueChanged);
            }
        }
        #endregion

        #region Interface builder
        public override void PrepareForInterfaceBuilder()
        {
            base.PrepareForInterfaceBuilder();
            CustomInitialization();
        }
        #endregion

    }
}

