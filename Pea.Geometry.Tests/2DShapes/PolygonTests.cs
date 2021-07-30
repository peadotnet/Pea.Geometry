using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pea.Geometry.Tests._2DShapes
{
	public class PolygonTests
	{
		public static Polygon CreateTestPolygon()
		{
			var points = new List<Vector2D>()
			{
				new Vector2D(0, 1),
				new Vector2D(-4, 0),
				new Vector2D(2, -3),
				new Vector2D(-1, -1)
			};

			return PolygonFactory.CreateByPoints(points);
		}
	}
}
