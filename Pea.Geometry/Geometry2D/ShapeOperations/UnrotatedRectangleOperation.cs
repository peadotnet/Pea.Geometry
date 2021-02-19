using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class UnrotatedRectangleOperation : IShapeOperation<Rectangle, Rectangle>
	{
		public double Distance(Rectangle shape1, Rectangle shape2)
		{
			double xDistance = XDistance(shape1, shape2);
			double yDistance = YDistance(shape1, shape2);

			return Math.Max(xDistance, yDistance);
		}

		public bool DoOverlap(Rectangle shape1, Rectangle shape2)
		{
			double xDistance = XDistance(shape1, shape2);
			double yDistance = YDistance(shape1, shape2);

			return (xDistance <= 0) && (yDistance <= 0);
		}

		public double ManhattanDistance(Rectangle shape1, Rectangle shape2)
		{
			double xDistance = XDistance(shape1, shape2);
			double yDistance = YDistance(shape1, shape2);

			double distance = 0;
			if (xDistance > 0) distance += xDistance;
			if (yDistance > 0) distance += yDistance;

			return distance;
		}

		private double YDistance(Rectangle shape1, Rectangle shape2)
		{
			return Math.Abs(shape1.Center.Y - shape2.Center.Y) - (shape1.Height + shape2.Height) / 2;
		}
		private double XDistance(Rectangle shape1, Rectangle shape2)
		{
			return Math.Abs(shape1.Center.X - shape2.Center.X) - (shape1.Width + shape2.Width) / 2;
		}

	}
}
