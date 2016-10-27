using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.Drawing;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using Foundation;

[assembly: ExportRenderer(typeof(InvisionDo1.ShapeView), typeof(InvisionDo1.iOS.ShapeRenderer))]
namespace InvisionDo1.iOS
{
    public class ShapeRenderer : VisualElementRenderer<ShapeView>
    {
        private readonly float QuarterTurnCounterClockwise = (float)(-1f * (Math.PI * 0.5f));

        public ShapeRenderer()
        {
        }
  
        public override void Draw(CGRect rect)
        {
            var currentContext = UIGraphics.GetCurrentContext();
            var properRect = AdjustForThickness(rect);
            HandleShapeDraw(currentContext, properRect);
	    }


        protected CGRect AdjustForThickness(CGRect rect)
        {
            var x = rect.X + Element.Padding.Left;
            var y = rect.Y + Element.Padding.Top;
            var width = rect.Width - Element.Padding.HorizontalThickness;
            var height = rect.Height - Element.Padding.VerticalThickness;
            return new RectangleF((float)x, (float)y, (float)width, (float)height);
        }

        protected virtual void HandleShapeDraw(CGContext currentContext, CGRect rect)
        {
            // Only used for circles
            var centerX = rect.X + (rect.Width / 2);
            var centerY = rect.Y + (rect.Height / 2);
            var radius = rect.Width / 2;
            var startAngle = 0;
            var endAngle = (float)(Math.PI * 2);

            switch (Element.ShapeType)
            {
                case ShapeType.Box:
                    HandleStandardDraw(currentContext, rect, () => {
                        if (Element.CornerRadius > 0)
                        {
                            var path = UIBezierPath.FromRoundedRect(rect, Element.CornerRadius);
                            currentContext.AddPath(path.CGPath);
                        }
                        else
                        {
                            currentContext.AddRect(rect);
                        }
                    });
                    break;
                case ShapeType.Circle:
                    HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, startAngle, endAngle, true));
                    break;
                case ShapeType.CircleIndicator:
                    HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, startAngle, endAngle, true));
                    HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, QuarterTurnCounterClockwise, (float)(Math.PI * 2 * (Element.IndicatorPercentage / 100)) + QuarterTurnCounterClockwise, false), Element.StrokeWidth, true);
                    break;
				case ShapeType.AnimatedCircularIndicator:
					HandleStandardDraw(currentContext, rect, () => drawShapeLayer(rect));
					break;
					          
            }
        }


		void drawShapeLayer(CGRect rect)
		{
			double width = rect.Width;
			double height = rect.Height;
			if (width <= 0 || height <= 0)
			{
				return;
			}
			double num = Math.Min(width, height);
			double num1 = (width > num ? (width - num) / 2 : 0);
			double num2 = (height > num ? (height - num) / 2 : 0);

			CAShapeLayer cAShapeLayerBase = new CAShapeLayer();
			cAShapeLayerBase.Path = CGPath.EllipseFromRect(new CGRect(num1, num2, num, num));
			cAShapeLayerBase.FillColor = Element.BackgroundColor.ToCGColor();
			cAShapeLayerBase.StrokeColor = Element.StrokeColor.ToCGColor();
			cAShapeLayerBase.LineWidth = Element.StrokeWidth;
			cAShapeLayerBase.Opacity = 0.5f;

			CAShapeLayer cAShapeLayer = new CAShapeLayer();
			cAShapeLayer.Path = CGPath.EllipseFromRect(new CGRect(num1, num2, num, num));
			cAShapeLayer.FillColor = Element.BackgroundColor.ToCGColor();
			cAShapeLayer.StrokeColor = Element.IndicatorColor.ToCGColor();
			cAShapeLayer.LineWidth = Element.StrokeWidth;
			//cAShapeLayer.StrokeStart = 0.5f;
			cAShapeLayer.StrokeEnd = 0.0f;
			this.Layer.AddSublayer(cAShapeLayerBase);
			this.Layer.AddSublayer(cAShapeLayer);


			CABasicAnimation drawAnimation = CABasicAnimation.FromKeyPath("strokeEnd");
			drawAnimation.Duration = 2.0;
			drawAnimation.RepeatCount = 1.0f;
			drawAnimation.From = NSNumber.FromFloat(0.0f);
			drawAnimation.To = NSNumber.FromFloat(1.0f);
			drawAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseIn);
			drawAnimation.RemovedOnCompletion = false;
			drawAnimation.Additive = true;
			drawAnimation.FillMode = CAFillMode.Forwards;
			//drawAnimation.AnimationStopped += DrawAnimation_AnimationStopped;
			// add the defined animation to the cirecle created
			cAShapeLayer.AddAnimation(drawAnimation, "drawCircleAnimation");

		}


        /// <summary>
        /// A simple method for handling our drawing of the shape. This method is called differently for each type of shape
        /// </summary>
        /// <param name="currentContext">Current context.</param>
        /// <param name="rect">Rect.</param>
        /// <param name="createPathForShape">Create path for shape.</param>
        /// <param name="lineWidth">Line width.</param>
        protected virtual void HandleStandardDraw(CGContext currentContext, CGRect rect, Action createPathForShape, float? lineWidth = null, bool isIndicator=false)
        {
            currentContext.SetLineWidth(lineWidth ?? Element.StrokeWidth);
            //currentContext.SetLineWidth(0);
            currentContext.SetFillColor(base.Element.Color.ToCGColor());
            
			if(!isIndicator)
				currentContext.SetStrokeColor(Element.StrokeColor.ToCGColor());
			else
				currentContext.SetStrokeColor(Element.IndicatorColor.MultiplyAlpha(0.5).ToCGColor());

            createPathForShape();

            currentContext.DrawPath(CoreGraphics.CGPathDrawingMode.FillStroke);
        }
    }
}