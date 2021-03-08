using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry2D
{
	public class RectangleFactory
	{
		public static Rectangle CreateByDimensions(double centerX, double centerY, double width, double height)
		{
			return new Rectangle(centerX, centerY, width, height);
		}

		public static  Rectangle CreateByAreaAndRatio(double centerX, double centerY, double area, double ratio)
		{
			var width = Math.Sqrt(area * ratio);
			var height = width / ratio;
			return new Rectangle(centerX, centerY, width, height);
		}
	}
}
