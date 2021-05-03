using System.Collections.Generic;

namespace Pea.Geometry.General
{
	public interface ITransformable<TC, TD>
		where TC : ITransformable<TC, TD>
		where TD : Vector, new()
	{
		IList<Matrix<TD>> Transformations { get; }
		TC Apply(Matrix<TD> transformation);
		TC Rotate(double angle);
		TC Scale(double xFactor, double yFactor);
		TC Translate(double xOffset, double yOffset);
		TC XMirror();
		TC YMirror();
		TC XSkew(double angle);
		TC YSkew(double angle);
	}
}
