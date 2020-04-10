namespace Pea.Geometry2D.Transformations
{
	public class Scaling2D : Transformation2D
	{ 
		public Scaling2D(double xFactor, double yFactor)
		{
			Values[0, 0] = xFactor;
			Values[1, 1] = yFactor;
			Values[2, 2] = 1;
		}
	}
}
