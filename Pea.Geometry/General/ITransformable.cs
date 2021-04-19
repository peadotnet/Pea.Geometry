using System.Collections.Generic;

namespace Pea.Geometry.General
{
	public interface ITransformable<T> where T : Vector, new()
	{
		IList<Matrix<T>> Transformations { get; }
		void Apply(Matrix<T> transformation);
		void Rotate(double angle);
		void Scale(double xFactor, double yFactor);
		void Translate(double xOffset, double yOffset);
		void XMirror();
		void YMirror();
		void XSkew(double angle);
		void YSkew(double angle);
	}
}
