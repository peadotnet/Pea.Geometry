using Pea.Geometry.General;
using Pea.Geometry2D;
using Pea.Geometry2D.Transformations;

namespace Pea.Geometry.Geometry2D
{
	public class TransformationFactory
	{
		public static Matrix<Vector2D> Rotation(double angle)
		{
			return new Rotation2D(angle);
		}

		public static Matrix<Vector2D> Scaling(double xFactor, double yFactor)
		{
			return new Scaling2D(xFactor, yFactor);
		}

		public static Matrix<Vector2D> Translation(double xOffset, double yOffset)
		{
			return new Translation2D(xOffset, yOffset);
		}

		public static Matrix<Vector2D> XMirror()
		{
			return new XMirror2D();
		}

		public static Matrix<Vector2D> YMirror()
		{
			return new YMirror2D();
		}

		public static Matrix<Vector2D> XSkew(double angle)
		{
			return new XSkew2D(angle);
		}

		public static Matrix<Vector2D> YSkew(double angle)
		{
			return new YSkew2D(angle);
		}
	}
}
