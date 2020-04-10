using System;

namespace Pea.Geometry2D.Transformations
{
	public class XSkew2D : Transformation2D
	{
		public XSkew2D(double angle)
		{
			Values[0, 0] = 1;
			Values[0, 1] = Math.Tan(angle);
			Values[1, 1] = 1;
			Values[2, 2] = 1;
		}
	}
}
