using System;

namespace Pea.Geometry2D.Transformations
{
	public class Rotation2D : Transformation2D
	{
		public Rotation2D(double angle)
		{
			Values[0, 0] = Math.Cos(angle);
			Values[0, 1] = Math.Sin(angle);
			Values[1, 0] = -1 * Math.Sin(angle);
			Values[1, 1] = Math.Cos(angle);
			Values[2, 2] = 1;
		}
	}
}
