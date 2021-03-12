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

			return PointIsInsideCircle(circle, xn, yn);
		}

		public override bool IsIncluded(Rectangle rectangle, Circle circle)
		{
			var points = rectangle.Points;
			for(int i=0;i<4; i++)
			{
				if (!PointIsInsideCircle(circle, points[i].X, points[i].Y))
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

		private static bool PointIsInsideCircle(Circle circle, double x, double y)
		{
			var dx = x - circle.Center.X;
			var dy = y - circle.Center.Y;
			return (dx * dx + dy * dy) < circle.Radius * circle.Radius;
		}
	}
}
