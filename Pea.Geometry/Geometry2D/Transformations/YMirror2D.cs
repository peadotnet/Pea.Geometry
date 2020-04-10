namespace Pea.Geometry2D.Transformations
{
	public class YMirror2D : Transformation2D
	{
		public YMirror2D()
		{
			Values[0, 0] = 1;
			Values[1, 1] = -1;
			Values[2, 2] = 1;
		}
	}
}
