using Pea.Geometry2D.Shapes;

namespace Pea.Geometry2D
{
	public class CircleFactory
	{
		public static Circle Create(double centerX, double centerY, double radius)
		{
			return new Circle(new Vector2D(centerX, centerY), radius);
		}
	}
}
