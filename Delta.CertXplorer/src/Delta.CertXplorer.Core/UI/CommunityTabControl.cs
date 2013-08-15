using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public class CommunityTabControl : CustomTabControl
    {
        private const int BEZIER_OUTER = 5;
        private const int BEZIER_INNER = 2;

        private int bezierInnerCurveOffset = BEZIER_INNER;
        private int bezierOuterCurveOffset = BEZIER_OUTER;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"),
         EditorBrowsable(EditorBrowsableState.Always), DefaultValue(BEZIER_INNER)]
        public int BezierInnerCurveOffset { get { return bezierInnerCurveOffset; } set { bezierInnerCurveOffset = value; } }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"),
         EditorBrowsable(EditorBrowsableState.Always), DefaultValue(BEZIER_OUTER)]
        public int BezierOuterCurveOffset { get { return bezierOuterCurveOffset; } set { bezierOuterCurveOffset = value; } }
        
        protected override GraphicsPath GetPath(int index)
        {
            Rectangle rect = base.GetTabRect(index);
            GraphicsPath gp = new GraphicsPath();

            Point ptBottomLeft = Point.Empty;
            Point ptTopLeft = Point.Empty;
            Point ptTopRight = Point.Empty;
            Point ptBottomRight = Point.Empty;

            Point ptB1 = Point.Empty;
            Point ptB2 = Point.Empty;
            Point ptB3 = Point.Empty;
            Point ptB4 = Point.Empty;

            switch (Alignment)
            {
                case TabAlignment.Top:
                    ptBottomLeft = new Point(rect.Left + 1, rect.Bottom);
                    ptTopLeft = new Point(rect.Left + 1, rect.Top);
                    ptTopRight = new Point(rect.Right, rect.Top);
                    ptBottomRight = new Point(rect.Right, rect.Bottom);

                    ptB1 = Point.Add(ptTopLeft, new Size(0, bezierOuterCurveOffset));
                    ptB2 = Point.Add(ptTopLeft, new Size(0, bezierInnerCurveOffset));
                    ptB3 = Point.Add(ptTopLeft, new Size(bezierInnerCurveOffset, 0));
                    ptB4 = Point.Add(ptTopLeft, new Size(bezierOuterCurveOffset, 0));

                    gp.AddLine(ptBottomLeft, ptB1);
                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);

                    ptB1 = Point.Add(ptTopRight, new Size(-bezierOuterCurveOffset, 0));
                    ptB2 = Point.Add(ptTopRight, new Size(-bezierInnerCurveOffset, 0));
                    ptB3 = Point.Add(ptTopRight, new Size(0, bezierInnerCurveOffset));
                    ptB4 = Point.Add(ptTopRight, new Size(0, bezierOuterCurveOffset));

                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);
                    gp.AddLine(ptB4, ptBottomRight);
                    break;

                case TabAlignment.Bottom:
                    ptBottomLeft = new Point(rect.Left + 1, rect.Bottom);
                    ptTopLeft = new Point(rect.Left + 1, rect.Top - 1);
                    ptTopRight = new Point(rect.Right, rect.Top - 1);
                    ptBottomRight = new Point(rect.Right, rect.Bottom);

                    ptB1 = Point.Add(ptBottomLeft, new Size(0, -bezierOuterCurveOffset));
                    ptB2 = Point.Add(ptBottomLeft, new Size(0, -bezierInnerCurveOffset));
                    ptB3 = Point.Add(ptBottomLeft, new Size(bezierInnerCurveOffset, 0));
                    ptB4 = Point.Add(ptBottomLeft, new Size(bezierOuterCurveOffset, 0));

                    gp.AddLine(ptTopLeft, ptB1);
                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);

                    ptB1 = Point.Add(ptBottomRight, new Size(-bezierOuterCurveOffset, 0));
                    ptB2 = Point.Add(ptBottomRight, new Size(-bezierInnerCurveOffset, 0));
                    ptB3 = Point.Add(ptBottomRight, new Size(0, -bezierInnerCurveOffset));
                    ptB4 = Point.Add(ptBottomRight, new Size(0, -bezierOuterCurveOffset));

                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);
                    gp.AddLine(ptB4, ptTopRight);
                    break;

                case TabAlignment.Left:
                    ptBottomLeft = new Point(rect.Left, rect.Bottom);
                    ptTopLeft = new Point(rect.Left, rect.Top + 1);
                    ptTopRight = new Point(rect.Right + 1, rect.Top + 1);
                    ptBottomRight = new Point(rect.Right + 1, rect.Bottom);

                    ptB1 = Point.Add(ptTopLeft, new Size(bezierOuterCurveOffset, 0));
                    ptB2 = Point.Add(ptTopLeft, new Size(bezierInnerCurveOffset, 0));
                    ptB3 = Point.Add(ptTopLeft, new Size(0, bezierInnerCurveOffset));
                    ptB4 = Point.Add(ptTopLeft, new Size(0, bezierOuterCurveOffset));

                    gp.AddLine(ptTopRight, ptB1);
                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);

                    ptB1 = Point.Add(ptBottomLeft, new Size(0, -bezierOuterCurveOffset));
                    ptB2 = Point.Add(ptBottomLeft, new Size(0, -bezierInnerCurveOffset));
                    ptB3 = Point.Add(ptBottomLeft, new Size(bezierInnerCurveOffset, 0));
                    ptB4 = Point.Add(ptBottomLeft, new Size(bezierOuterCurveOffset, 0));

                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);
                    gp.AddLine(ptB4, ptBottomRight);
                    break;

                case TabAlignment.Right:
                    ptBottomLeft = new Point(rect.Left - 2, rect.Bottom);
                    ptTopLeft = new Point(rect.Left - 2, rect.Top + 1);
                    ptTopRight = new Point(rect.Right, rect.Top + 1);
                    ptBottomRight = new Point(rect.Right, rect.Bottom);

                    ptB1 = Point.Add(ptTopRight, new Size(-bezierOuterCurveOffset, 0));
                    ptB2 = Point.Add(ptTopRight, new Size(-bezierInnerCurveOffset, 0));
                    ptB3 = Point.Add(ptTopRight, new Size(0, bezierInnerCurveOffset));
                    ptB4 = Point.Add(ptTopRight, new Size(0, bezierOuterCurveOffset));

                    gp.AddLine(ptTopLeft, ptB1);
                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);

                    ptB1 = Point.Add(ptBottomRight, new Size(0, -bezierOuterCurveOffset));
                    ptB2 = Point.Add(ptBottomRight, new Size(0, -bezierInnerCurveOffset));
                    ptB3 = Point.Add(ptBottomRight, new Size(-bezierInnerCurveOffset, 0));
                    ptB4 = Point.Add(ptBottomRight, new Size(-bezierOuterCurveOffset, 0));

                    gp.AddBezier(ptB1, ptB2, ptB3, ptB4);
                    gp.AddLine(ptB4, ptBottomLeft);
                    break;
            }

            return gp;
        }
    }
}
