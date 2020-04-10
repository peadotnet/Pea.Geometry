using Pea.Geometry2D.Shapes;
using System.Collections.Generic;

namespace Pea.Geometry2D
{
	public class PolygonFactory
	{
		public static Polygon CreateByPoints(IEnumerable<Vector2D> points)
		{
			return new Polygon(points);
		}
	}
}
