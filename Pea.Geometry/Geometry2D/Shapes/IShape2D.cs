using Pea.Geometry.General;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public interface IShape2D : IDeepCloneable<IShape2D>, ITransformable<Vector2D>
	{
		Vector2D Center { get; set; }
		List<Vector2D> Points { get; }
		void DoTransform();
		void Invalidate();
		IShape2D DoOffset(double marginWidth);
	}
}
