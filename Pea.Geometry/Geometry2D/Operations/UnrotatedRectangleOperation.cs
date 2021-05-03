using Pea.Geometry2D.Shapes;

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

		public override bool DoOverlapWithMargin(Rectangle rectangle1, Rectangle rectangle2)
		{
			var margin = Max(rectangle1.MarginWidth, rectangle2.MarginWidth);

			double xDistance = XDistance(rectangle1, rectangle2);
			double yDistance = YDistance(rectangle1, rectangle2);

			return (xDistance <= margin) && (yDistance <= margin);
		}

		public override bool IsIncluded(Rectangle shape1, Rectangle shape2)
		{
			return (shape1.Left > shape2.Left && shape1.Right < shape2.Right && shape1.Bottom > shape2.Bottom && shape1.Top < shape2.Top);
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
	}
}
