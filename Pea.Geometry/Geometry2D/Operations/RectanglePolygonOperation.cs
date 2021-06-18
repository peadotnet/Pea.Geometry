using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class RectanglePolygonOperation : ShapeOperationBase<Rectangle, Polygon>
	{
		public enum ShapeRelation
		{
			ToBeCalculated = -1,
			Outside = 0,
			Inside = 1,
			Intersected = 2
		}

		public override double Distance(Rectangle rectangle, Polygon polygon)
		{
			return (rectangle.Center - polygon.Center).GetLength();
		}

		public override bool DoOverlap(Rectangle rectangle, Polygon polygon)
		{
			if (!DoOverlapWithBoundaryRectangle(rectangle, polygon.BoundingRectangle, 0)) return false;

			var checkByPoints = CheckPolygonPoints(rectangle, polygon);
			if (checkByPoints == ShapeRelation.Outside) return false;
			if (checkByPoints == ShapeRelation.Intersected) return true;

			checkByPoints = AnyRectanglePointIsInside(rectangle, polygon);
			if (checkByPoints == ShapeRelation.Intersected) return true;

			return AnySideIntersects(rectangle, polygon);
		}

		public override bool DoOverlapWithMargin(Rectangle rectangle, Polygon polygon)
		{
			double marginWidth = Max(rectangle.MarginWidth, polygon.MarginWidth);

			if (!DoOverlapWithBoundaryRectangle(rectangle, polygon.BoundingRectangle, marginWidth)) return false;

			var checkByPoints = CheckPolygonPoints(rectangle.Margin, polygon);
			if (checkByPoints == ShapeRelation.Intersected) return true;

			checkByPoints = AnyRectanglePointIsInside(rectangle.Margin, polygon);
			if (checkByPoints == ShapeRelation.Intersected) return true;

			if (AnySideIntersects(rectangle.Margin, polygon)) return true;

			if (AnyPolygonPointIsCloserThanMargin(rectangle, polygon, marginWidth)) return true;

			if (AnyRectanglePointIsCloserThanMargin(rectangle, polygon, marginWidth)) return true;

			return false;
		}

		private bool DoOverlapWithBoundaryRectangle(Rectangle rectangle, Rectangle polygonBoundary, double marginWidth)
		{
			double xDistance = XDistance(rectangle, polygonBoundary);
			double yDistance = YDistance(rectangle, polygonBoundary);

			return (xDistance <= marginWidth) && (yDistance <= marginWidth);
		}

		private double YDistance(Rectangle shape1, Rectangle shape2)
		{
			return Abs(shape1.Center.Y - shape2.Center.Y) - (shape1.Height + shape2.Height) / 2;
		}
		private double XDistance(Rectangle shape1, Rectangle shape2)
		{
			return Abs(shape1.Center.X - shape2.Center.X) - (shape1.Width + shape2.Width) / 2;
		}

		public override bool IsIncluded(Rectangle rectangle, Polygon polygon)
		{
			var checkByPoints = CheckPolygonPoints(rectangle.Margin, polygon);
			if (checkByPoints == ShapeRelation.Outside) return false;
			if (checkByPoints == ShapeRelation.Intersected) return false;

			if (AnyRectanglePointIsOutside(rectangle.Margin, polygon)) return false;

			return !AnySideIntersects(rectangle.Margin, polygon);
		}

		public ShapeRelation CheckPolygonPoints(Rectangle rectangle, Polygon polygon)
		{
			var left = rectangle.Left;
			var right = rectangle.Right;
			var top = rectangle.Top;
			var bottom = rectangle.Bottom;

			var minX = double.MaxValue;
			var minY = double.MaxValue;
			var maxX = double.MinValue;
			var maxY = double.MinValue;

			for (int i=0; i< polygon.Points.Count; i++)
			{
				var point = polygon.Points[i];
				if (point.X >= left && point.X <= right && point.Y >= bottom && point.Y <= top) return ShapeRelation.Intersected;

				if (point.X < minX) minX = point.X;
				if (point.X > maxX) maxX = point.X;
				if (point.Y < minY) minY = point.Y;
				if (point.Y > maxY) maxY = point.Y;
			}

			if (maxX < left || minX > right || maxY < bottom || minY > top) return ShapeRelation.Outside;

			return ShapeRelation.ToBeCalculated;
		}

		public ShapeRelation AnyRectanglePointIsInside(Rectangle rectangle, Polygon polygon)
		{
			var points = rectangle.Points;
			for(int i=0; i < 4; i++)
			{
				if (cn_PnPoly(points[i], polygon)) return ShapeRelation.Intersected;
			}
			return ShapeRelation.ToBeCalculated;
		}

		public bool AnyRectanglePointIsOutside(Rectangle rectangle, Polygon polygon)
		{
			var points = rectangle.Points;
			for (int i = 0; i < 4; i++)
			{
				if (!cn_PnPoly(points[i], polygon)) return true;
			}
			return false;
		}

		public bool AnyRectanglePointIsCloserThanMargin(Rectangle rectangle, Polygon polygon, double marginWith)
		{
			var points = rectangle.Points;
			for(int r = 0; r < 4; r++)
			{
				for (int p=0; p < polygon.Points.Count - 1; p++)
				{
					if (IsSectionCloserToPoint(polygon.Points[p], polygon.Points[p + 1], points[r], marginWith)) return true;
				}
			}
			return false;
		}

		public bool AnyPolygonPointIsCloserThanMargin(Rectangle rectangle, Polygon polygon, double marginWidth)
		{
			var points = rectangle.Points;
			for (int p = 0; p < polygon.Points.Count - 1; p++)
			{
				for (int r = 0; r < 4; r++)
				{
					if (IsSectionCloserToPoint(points[r], polygon.Points[p], points[r + 1], marginWidth)) return true;
				}
			}
			return false;
		}

		public bool IsInsideBounding(Vector2D point, Polygon polygon)
		{
			var rectangle = polygon.BoundingRectangle;
			if (point.X >= rectangle.Left && point.X <= rectangle.Right && point.Y >= rectangle.Bottom && point.Y <= rectangle.Top) return true;

			return false;
		}

		public bool cn_PnPoly(Vector2D point, Polygon polygon)
		{
			var n = polygon.Points.Count;
			int count = 0;

			for (int i = 0; i < n; i++)
			{
				var next = (i + 1) % n;
				if (((polygon.Points[i].Y <= point.Y) && (polygon.Points[next].Y > point.Y))
				 || ((polygon.Points[i].Y > point.Y) && (polygon.Points[next].Y <= point.Y)))
				{
					var vt = (point.Y - polygon.Points[i].Y) / (polygon.Points[next].Y - polygon.Points[i].Y);
					if (point.X < polygon.Points[i].X + vt * (polygon.Points[next].X - polygon.Points[i].X))
						count++;
				}
			}
			return ((count & 1) == 1);
		}

		public bool AnySideIntersects(Rectangle rectangle, Polygon polygon)
		{
			var n = polygon.Points.Count - 1;
			for(int r = 0; r < 4; r++)
			{
				for(int p = 0; p < n; p++)
				{
					if (VectorHelper.DoIntersect(rectangle.Points[r], rectangle.Points[r + 1], polygon.Points[p], polygon.Points[p + 1]))
					{
						return true;
					}
				}
			}

			return false;
		}

		public override double OverlappingArea(Rectangle shape1, Polygon shape2)
		{
			throw new NotImplementedException();
		}
	}
}
