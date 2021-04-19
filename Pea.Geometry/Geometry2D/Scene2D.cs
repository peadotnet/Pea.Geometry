using Pea.Geometry2D.Shapes;
using System.Collections.Generic;

namespace Pea.Geometry.Geometry2D
{
	public class Scene2D : Transformable2DBase
	{
		public Scene2D()
		{
		}

		public IList<IShape2D> Shapes { get; } = new List<IShape2D>();

		void DoTransform()
		{
			var transformation = ComposeTransformations();
			for (int s = 0; s < Shapes.Count; s++)
			{
				Shapes[s].Apply(transformation);
			}
		}

		public IShape2D this[int index] => Shapes[index];
	}
}
