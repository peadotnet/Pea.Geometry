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

		public bool DoOverlap(Rectangle shape1, Polygon shape2)
		{
			throw new NotImplementedException();
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

		public bool IsInside(double x, double y, Polygon polygon)
		{
			int counter = 0;

			var n = polygon.Points.Count;
			var point1 = polygon.Points[0];
			for (int i = 1; i <= n; i++)
			{
				var point2 = polygon.Points[i % n];
				if (y > Min(point1.Y, point2.Y))
				{
					if (y <= Max(point1.Y, point2.Y))
					{
						if (point1.Y != point2.Y)
						{
							var xIntersection = (y - point1.Y) * (point2.X - point1.X) / (point2.Y - point1.Y) + point1.X;
							if (x <= Max(point1.X, point2.X))
							{
								if (point1.X == point2.X || x <= xIntersection)
									counter++;
							}
						}
					}
				}
				point1 = point2;
			}

			return (counter % 2 == 1);
		}

		// This code is patterned after [Franklin, 2000]
		public bool cn_PnPoly(Vector2D point, Polygon polygon)
		{
			var n = polygon.Points.Count - 1;
			int count = 0;

			for (int i = 0; i < n; i++)
			{
				if (((polygon.Points[i].Y <= point.Y) && (polygon.Points[i + 1].Y > point.Y))
				 || ((polygon.Points[i].Y > point.Y) && (polygon.Points[i + 1].Y <= point.Y)))
				{
					var vt = (point.Y - polygon.Points[i].Y) / (polygon.Points[i + 1].Y - polygon.Points[i].Y);
					if (point.X < polygon.Points[i].X + vt * (polygon.Points[i + 1].X - polygon.Points[i].X))
						count++;
				}
			}
			return ((count & 1) == 1);
		}

		public bool wn_PnPoly(Vector2D point, Polygon polygon)
		{
			var n = polygon.Points.Count - 1;
			int count = 0;

			for (int i = 0; i < n; i++)
			{
				if (polygon.Points[i].Y <= point.Y)
				{
					if (polygon.Points[i + 1].Y > point.Y)
					{
						if (IsLeft(polygon.Points[i], polygon.Points[i + 1], point) > 0) count++;
					}
				}
				else
				{
					if (polygon.Points[i + 1].Y <= point.Y)
					{
						if (IsLeft(polygon.Points[i], polygon.Points[i + 1], point) < 0) count--;
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
