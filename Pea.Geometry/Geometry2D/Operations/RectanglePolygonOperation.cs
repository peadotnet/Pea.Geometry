using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class RectanglePolygonOperation : IShapeOperation<Rectangle, Polygon>
	{
		public enum ShapeRelation
		{
			ToBeCalculated = -1,
			Outside = 0,
			Inside = 1,
			Intersected = 2
		}

		public double Distance(Rectangle rectangle, Polygon polygon)
		{
			throw new NotImplementedException();
		}

		public bool DoOverlap(Rectangle rectangle, Polygon polygon)
		{
			var checkByPoints = CheckPolygonPoints(rectangle, polygon);
			if (checkByPoints == ShapeRelation.Outside) return false;
			if (checkByPoints == ShapeRelation.Intersected) return true;

			checkByPoints = CheckRectanglePoints(rectangle, polygon);
			if (checkByPoints == ShapeRelation.Intersected) return true;

			return DoAnySideIntersect(rectangle, polygon);
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

		public ShapeRelation CheckRectanglePoints(Rectangle rectangle, Polygon polygon)
		{
			var points = rectangle.Points;
			for(int i=0; i< points.Count; i++)
			{
				if (wn_PnPoly(points[i], polygon)) return ShapeRelation.Intersected;
			}
			return ShapeRelation.ToBeCalculated;
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
					var vt = (point.Y - polygon.Points[i].Y) / (polygon.Points[i + 1].Y - polygon.Points[i].Y);
					if (point.X < polygon.Points[i].X + vt * (polygon.Points[next].X - polygon.Points[i].X))
						count++;
				}
			}
			return ((count & 1) == 1);
		}

		public bool wn_PnPoly(Vector2D point, Polygon polygon)
		{
			var n = polygon.Points.Count;
			int count = 0;

			for (int i = 0; i < n; i++)
			{
				var next = (i + 1) % n;
				if (polygon.Points[i].Y <= point.Y)
				{
					if (polygon.Points[next].Y > point.Y)
					{
						if (IsLeft(polygon.Points[i], polygon.Points[next], point) > 0) count++;
					}
				}
				else
				{
					if (polygon.Points[next].Y <= point.Y)
					{
						if (IsLeft(polygon.Points[i], polygon.Points[next], point) < 0) count--;
					}
				}
			}
			return count != 0;
		}

		public double IsLeft(Vector2D P0, Vector2D P1, Vector2D P2)
		{
			return ((P1.X - P0.X) * (P2.Y - P0.Y)
					- (P2.X - P0.X) * (P1.Y - P0.Y));
		}

		public bool DoAnySideIntersect(Rectangle rectangle, Polygon polygon)
		{
			var n = polygon.Points.Count;
			for(int r=0; r<=4; r++)
			{
				int nextr = (r + 1) % 4;
				for(int p=0; p<=n; p++)
				{
					int nextp = (p + 1) % 4;
					if (SegmentOperation.DoIntersect(rectangle.Points[r], rectangle.Points[nextr], polygon.Points[p], polygon.Points[nextp]))
					{
						return true;
					}	
				}
			}

			return false;
		}

		public double EuclideanDistance(Rectangle shape1, Polygon shape2)
		{
			throw new NotImplementedException();
		}

		public double ManhattanDistance(Rectangle shape1, Polygon shape2)
		{
			throw new NotImplementedException();
		}

		public double OverlappingArea(Rectangle shape1, Polygon shape2)
		{
			throw new NotImplementedException();
		}

		private double Min(double x, double y)
		{
			return x < y ? x : y;
		}

		private double Max(double x, double y)
		{
			return x > y ? x : y;
		}
	}
}
