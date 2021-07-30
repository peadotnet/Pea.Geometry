using Pea.Geometry.General;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public interface IShape2D : IDeepCloneable<IShape2D>, ITransformable<IShape2D, Vector2D>
	{
		Vector2D Center { get; set; }
		Rectangle BoundingRectangle { get; }
		List<Vector2D> Points { get; }
		double MarginWidth { get; set; }
		void DoTransform();
		void Invalidate();
		IShape2D DoOffset(double marginWidth);
	}
}
