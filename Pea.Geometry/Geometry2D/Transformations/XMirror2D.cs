namespace Pea.Geometry2D.Transformations
{
	public class XMirror2D : Transformation2D
	{
		public XMirror2D()
		{
			Values[0, 0] = -1;
			Values[1, 1] = 1;
			Values[2, 2] = 1;
		}
	}
}
