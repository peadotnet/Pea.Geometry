using Pea.Core;
using Pea.Geometry2D.Transformations;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public interface IShape2D
	{
		Vector2D Center { get; set; }
		List<Vector2D> Points { get; }
		IList<Transformation2D> Transformations { get; }
		void DoTransform();
		void Invalidate();
	}
}
