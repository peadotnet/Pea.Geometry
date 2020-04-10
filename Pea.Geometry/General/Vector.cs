namespace Pea.Geometry.General
{
	public class Vector
	{
		public int Dimensions { get; }
		public double[] Coordinates { get; }

		public Vector(int dimensions)
		{
			Dimensions = dimensions;
			Coordinates = new double[dimensions];
		}
	}
}
