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

		public override string ToString()
		{
			var sb = new System.Text.StringBuilder();
			for (int i=0; i< Coordinates.Length; i++)
			{
				sb.Append(Coordinates[i].ToString("#.##") + " ");
			}
			return sb.ToString();
		}
	}
}
