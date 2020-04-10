using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public class Rectangle : ShapeBase
	{
		public double Width { get; }
		public double Height { get; }
		public double Area { get; }
		public double Ratio { get; }



		public Rectangle(double centerX, double centerY, double width, double height)
		{
			Center = new Vector2D(centerX, centerY);
			Width = width;
			Height = height;
			Area = width * height;
			Ratio = height / width;
		}
	}
}
