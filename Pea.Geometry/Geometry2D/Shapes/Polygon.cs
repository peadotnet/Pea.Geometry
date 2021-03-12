using Pea.Core;
using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Polygon : ShapeBase, IDeepCloneable<Polygon>
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

		public Polygon DeepClone()
		{
			return new Polygon(Points);
		}

		public override void Invalidate()
		{
		}
	}
}
