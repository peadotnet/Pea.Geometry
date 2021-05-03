using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class RectangleCircleOperation : ShapeOperationBase<Rectangle, Circle>
	{
		public override double Distance(Rectangle rectangle, Circle circle)
		{
			var xn = Max(rectangle.Left, Min(circle.Center.X, rectangle.Right));
			var yn = Max(rectangle.Bottom, Min(circle.Center.Y, rectangle.Top));

			var dx = xn - circle.Center.X;
			var dy = yn - circle.Center.Y;
			return Math.Sqrt(dx * dx + dy * dy) - circle.Radius;
		}

		public override bool DoOverlap(Rectangle rectangle, Circle circle)
		{
			var xn = Max(rectangle.Left, Min(circle.Center.X, rectangle.Right));
			var yn = Max(rectangle.Bottom, Min(circle.Center.Y, rectangle.Top));

			return PointIsInsideCircle(circle.Center, circle.Radius, xn, yn);
		}

		public override bool DoOverlapWithMargin(Rectangle rectangle, Circle circle)
		{
			if (rectangle.MarginWidth > circle.MarginWidth)
			{
				var xn = Max(rectangle.Left - rectangle.MarginWidth, Min(circle.Center.X, rectangle.Right + rectangle.MarginWidth));
				var yn = Max(rectangle.Bottom - rectangle.MarginWidth, Min(circle.Center.Y, rectangle.Top + rectangle.MarginWidth));

				return PointIsInsideCircle(circle.Center, circle.Radius, xn, yn);
			}
			else
			{
				var xn = Max(rectangle.Left, Min(circle.Center.X, rectangle.Right));
				var yn = Max(rectangle.Bottom, Min(circle.Center.Y, rectangle.Top));

				return PointIsInsideCircle(circle.Center, circle.Radius + circle.MarginWidth, xn, yn);
			}
		}

		public override bool IsIncluded(Rectangle rectangle, Circle circle)
		{
			var points = rectangle.Points;
			for(int i=0;i<4; i++)
			{
				if (!PointIsInsideCircle(circle.Center, circle.Radius, points[i].X, points[i].Y))
				{
					return false;
				}
			}
			return true;
		}

		public override double OverlappingArea(Rectangle rectangle, Circle circle)
		{
			throw new NotImplementedException();
		}

		private static bool PointIsInsideCircle(Vector2D circleCenter, double radius, double pX, double pY)
		{
			var dx = pX - circleCenter.X;
			var dy = pY - circleCenter.Y;
			return (dx * dx + dy * dy) < radius * radius;
		}
	}
}
