using System;

namespace Pea.Geometry2D.Transformations
{
	public class YSkew2D : Transformation2D
	{
		public YSkew2D(double angle)
		{
			Values[0, 0] = 1;
			Values[1, 0] = Math.Tan(angle);
			Values[1, 1] = 1;
			Values[2, 2] = 1;
		}
	}
}
