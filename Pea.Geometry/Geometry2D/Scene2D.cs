using Pea.Geometry2D.Shapes;
using System.Collections;
using System.Collections.Generic;

namespace Pea.Geometry.Geometry2D
{
	public class Scene2D : Transformable2DBase<Scene2D>, IEnumerable<IShape2D>
	{
		public Scene2D()
		{
		}

		public IList<IShape2D> Shapes { get; } = new List<IShape2D>();

		public void DoTransform()
		{
			var transformation = ComposeTransformations();
			for (int s = 0; s < Shapes.Count; s++)
			{
				Shapes[s].Apply(transformation);
				Shapes[s].DoTransform();
			}
		}

		public int Count => Shapes.Count;
		public IShape2D this[int index] => Shapes[index];

		public void Add(IShape2D shape)
		{
			Shapes.Add(shape);
		}

		public IEnumerator<IShape2D> GetEnumerator()
		{
			return Shapes.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Shapes.GetEnumerator();
		}
	}
}
