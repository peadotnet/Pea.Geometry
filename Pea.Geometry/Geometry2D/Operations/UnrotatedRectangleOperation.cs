using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class UnrotatedRectangleOperation : ShapeOperationBase<Rectangle, Rectangle>
	{
		public override double Distance(Rectangle shape1, Rectangle shape2)
		{
			double xDistance = XDistance(shape1, shape2);
			double yDistance = YDistance(shape1, shape2);

			return Max(xDistance, yDistance);
		}

		public override bool DoOverlap(Rectangle shape1, Rectangle shape2)
		{
			double xDistance = XDistance(shape1, shape2);
			double yDistance = YDistance(shape1, shape2);

			return (xDistance <= 0) && (yDistance <= 0);
		}

		private double YDistance(Rectangle shape1, Rectangle shape2)
		{
			return Abs(shape1.Center.Y - shape2.Center.Y) - (shape1.Height + shape2.Height) / 2;
		}
		private double XDistance(Rectangle shape1, Rectangle shape2)
		{
			return Abs(shape1.Center.X - shape2.Center.X) - (shape1.Width + shape2.Width) / 2;
		}

		public override double OverlappingArea(Rectangle shape1, Rectangle shape2)
		{
			var xOverlap = Min(shape1.Right, shape2.Right) - Max(shape1.Left, shape2.Left);
			if (xOverlap < 0) xOverlap = 0;
			var yOverlap = Min(shape1.Top, shape2.Top) - Max(shape1.Bottom, shape2.Bottom);
			if (yOverlap < 0) yOverlap = 0;

			return xOverlap * yOverlap;
		}

		public override double EuclideanDistanceOfCenters(Rectangle shape1, Rectangle shape2)
		{
			double xDistance = Abs(shape1.Center.X - shape2.Center.X);
			double yDistance = Abs(shape1.Center.Y - shape2.Center.Y);

			return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
		}

		public override double ManhattanDistanceOfCenters(Rectangle shape1, Rectangle shape2)
		{

			double xDistance = Abs(shape1.Center.X - shape2.Center.X);
			double yDistance = Abs(shape1.Center.Y - shape2.Center.Y);
			return xDistance + yDistance;
		}

		private double Min(double x, double y)
		{
			return x < y ? x : y;
		}

		private double Max(double x, double y)
		{
			return x > y ? x : y;
		}

		private double Abs(double val)
		{
			return val < 0 ? -1 * val : val;
		}
	}
}
