using Pea.Geometry.General;
using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Polygon : ShapeBase
	{
		private Rectangle _boundingRectangle = null;
		public Rectangle BoundingRectangle
		{
			get
			{
				if (_boundingRectangle == null) _boundingRectangle = CreateBoundingRectangle();
				return _boundingRectangle;
			}
		}

		internal Polygon(List<Vector2D> points)
		{
			int count = points.Count;
			if (count < 3) throw new ArgumentException($"The polygon has to have at least 3 points!");

			double sumX = 0;
			double sumY = 0;

			Points = new List<Vector2D>();

			for (int i=0; i< points.Count; i++)
			{
				Points.Add(new Vector2D(points[i].X, points[i].Y));
				sumX += points[i].X;
				sumY += points[i].Y;
			}

			Center = new Vector2D(sumX / count, sumY / count);

			var first = Points[0];
			var last = Points[count - 1];
			if (first.X != last.X || last.Y != last.Y)
			{
				Points.Add(new Vector2D(first.X, first.Y));
			}
		}

		public double GetArea()
		{
			double area = 0;
			int previous = Points.Count - 1;

			for (int current = 0; current < Points.Count; current++)
			{
				area += Points[previous].X * Points[current].Y - Points[previous].Y * Points[current].X;
			}
			return area / 2;
		}

		public Rectangle CreateBoundingRectangle()
		{
			double xMin = double.MaxValue;
			double xMax = double.MinValue;
			double yMin = double.MaxValue;
			double yMax = double.MinValue;

			for(int i=0; i < Points.Count; i++)
			{
				if (Points[i].X < xMin) xMin = Points[i].X;
				if (Points[i].X > xMax) xMax = Points[i].X;
				if (Points[i].Y < yMin) yMin = Points[i].Y;
				if (Points[i].Y > yMax) yMax = Points[i].Y;
			}

			return new Rectangle((xMin + xMax)/2, (yMin + yMax)/2, xMax - xMin, yMax - yMin);
		}

		public bool IsConvex()
		{
			throw new NotImplementedException();
			//public bool PolygonIsConvex()
			//{
			//	// For each set of three adjacent points A, B, C,
			//	// find the cross product AB · BC. If the sign of
			//	// all the cross products is the same, the angles
			//	// are all positive or negative (depending on the
			//	// order in which we visit them) so the polygon
			//	// is convex.
			//	bool got_negative = false;
			//	bool got_positive = false;
			//	int num_points = Points.Length;
			//	int B, C;
			//	for (int A = 0; A < num_points; A++)
			//	{
			//		B = (A + 1) % num_points;
			//		C = (B + 1) % num_points;

			//		float cross_product =
			//			CrossProductLength(
			//				Points[A].X, Points[A].Y,
			//				Points[B].X, Points[B].Y,
			//				Points[C].X, Points[C].Y);
			//		if (cross_product < 0)
			//		{
			//			got_negative = true;
			//		}
			//		else if (cross_product > 0)
			//		{
			//			got_positive = true;
			//		}
			//		if (got_negative && got_positive) return false;
			//	}

			//	// If we got this far, the polygon is convex.
			//	return true;
			//}

			// Return the cross product AB x BC.
			// The cross product is a vector perpendicular to AB
			// and BC having length |AB| * |BC| * Sin(theta) and
			// with direction given by the right-hand rule.
			// For two vectors in the X-Y plane, the result is a
			// vector with X and Y components 0 so the Z component
			// gives the vector's length and direction.
			//public static float CrossProductLength(float Ax, float Ay,
			//	float Bx, float By, float Cx, float Cy)
			//{
			//	// Get the vectors' coordinates.
			//	float BAx = Ax - Bx;
			//	float BAy = Ay - By;
			//	float BCx = Cx - Bx;
			//	float BCy = Cy - By;

			//	// Calculate the Z coordinate of the cross product.
			//	return (BAx * BCy - BAy * BCx);
			//}
		}

		public override void Invalidate()
		{
		}

		public override IShape2D DoOffset(double marginWidth)
		{
			throw new NotImplementedException();
		}

		public override IShape2D DeepClone()
		{
			return new Polygon(Points);
		}
	}
}
