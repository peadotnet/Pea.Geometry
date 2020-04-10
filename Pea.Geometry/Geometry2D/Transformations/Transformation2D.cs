using Pea.Geometry.General;

namespace Pea.Geometry2D.Transformations
{ 
	public abstract class Transformation2D : Matrix
	{
		public Transformation2D() : base(3)
		{

		}
		public static Transformation2D operator *(Transformation2D transformation1, Transformation2D transformation2)
		{
			return ((Matrix)transformation1 * (Matrix)transformation2) as Transformation2D;
		}
	}
}
