using Pea.Core;
using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Polygon : ShapeBase, IDeepCloneable<Polygon>
	{
//		private new List<Vector2D> Points { get; } = new List<Vector2D>();

		internal Polygon(IEnumerable<Vector2D> points)
		{
			double sumX = 0;
			double sumY = 0;
			int count = 0;

			//TODO: refactor this to for cycle
			foreach(var point in points)
			{
				Points.Add(new Vector2D(point.X, point.Y));
				sumX += point.X;
				sumY += point.Y;
				count++;
			}

			if (count < 1) throw new ArgumentException(nameof(points) + $" has {0} points!");

			Center = new Vector2D(sumX / count, sumY / count);
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
		public Polygon DeepClone()
		{
			return new Polygon(Points);
		}
	}
}
