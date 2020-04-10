using Pea.Geometry2D.Shapes;
using Pea.Geometry2D.Transformations;
using System.Collections.Generic;

namespace Pea.Geometry.Geometry2D
{
	public class Scene2D
	{
		public Scene2D()
		{
		}

		public IList<IShape2D> Shapes { get; } = new List<IShape2D>();

		public IList<Transformation2D> Transformations { get; } = new List<Transformation2D>();

		void DoTransform()
		{
			for (int s = 0; s < Shapes.Count; s++)
			{
				for (int t = 0; t < Transformations.Count; t++)
				{
					Shapes[s].Transformations.Add(Transformations[t]);
				}
				Shapes[s].DoTransform();
			}
		}
	}
}
