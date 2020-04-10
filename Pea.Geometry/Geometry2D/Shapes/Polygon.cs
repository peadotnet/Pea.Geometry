﻿using System;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Polygon : ShapeBase
	{
		private List<Vector2D> Points { get; } = new List<Vector2D>();

		internal Polygon(IEnumerable<Vector2D> points)
		{
			double sumX = 0;
			double sumY = 0;
			int count = 0;

			foreach(var point in points)
			{
				Points.Add(new Vector2D(point.X, point.Y));
				sumX += point.X;
				sumY += point.Y;
				count++;
			}

			if (count < 1) throw new ArgumentException(nameof(points));

			Center = new Vector2D(sumX / count, sumY / count);
		}
	}
}