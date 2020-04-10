namespace Pea.Geometry2D.Transformations
{
	public class Translation2D : Transformation2D
	{
		public Translation2D(double xOffset, double yOffset)
		{
			Values[0, 0] = 1;
			Values[1, 1] = 1;
			Values[0, 2] = xOffset;
			Values[1, 2] = yOffset;
			Values[2, 2] = 1;
		}
	}
}
